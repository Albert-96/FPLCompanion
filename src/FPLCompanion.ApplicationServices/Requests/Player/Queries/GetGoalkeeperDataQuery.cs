using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using FPLCompanion.Data.ViewModels;
using FPLCompanion.DataService.Abstractions;
using FPLCompanion.Dependencies;
using FPLCompanion.Dto;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLCompanion.ApplicationServices.Requests.Player.Queries
{
    public class GetGoalkeeperDataQuery : IRequest<LoadResult>
    {
        public DataSourceLoadOptions loadOptions { get; set; }
    }

    public class GetGoalkeeperDataQueryHandler : IRequestHandler<GetGoalkeeperDataQuery, LoadResult>
    {
        private readonly IElementDataService _elementDataService;
        private readonly ITeamDataService _teamDataService;
        private readonly IElementTypeDataService _elementTypeDataService;
        private readonly IMapper _mapper;

        public GetGoalkeeperDataQueryHandler(
            IElementDataService elementDataService,
            ITeamDataService teamDataService,
            IElementTypeDataService elementTypeDataService,
            IMapper mapper)
        {
            _elementDataService = elementDataService;
            _teamDataService = teamDataService;
            _elementTypeDataService = elementTypeDataService;
            _mapper = mapper;
        }

        public async Task<LoadResult> Handle(GetGoalkeeperDataQuery request, CancellationToken cancellationToken)
        {
            request.loadOptions.PrimaryKey = new[] { "id" };
            request.loadOptions.PaginateViaPrimaryKey = true;

            var playerEntities = await
                (
                    _elementDataService._elementsCollection.
                        Aggregate().
                        Match(x => x.element_type == (short)Position.Goalkeeper).
                        Lookup(_teamDataService._teamsCollection, x => x.team_code, y => y.code, (ElementAggregate p) => p.teamsInfo).
                        Lookup(_elementTypeDataService._elementTypeCollection, x => x.element_type, y => y.id, (ElementAggregate p) => p.positionsInfo)
                ).ToListAsync();
            var playerData = _mapper.Map<IEnumerable<ElementAggregate>, IEnumerable<ElementDto>>(playerEntities);

            LoadResult result = DataSourceLoader.Load(playerData, request.loadOptions);
            return result;
        }
    }
}
