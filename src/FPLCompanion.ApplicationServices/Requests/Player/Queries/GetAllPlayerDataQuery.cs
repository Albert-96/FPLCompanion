using AutoMapper;
using FPLCompanion.Data.Entities;
using FPLCompanion.Data.ViewModels;
using FPLCompanion.DataService.Abstractions;
using MediatR;
using MongoDB.Driver;

namespace FPLCompanion.ApplicationServices.Requests.Player.Queries
{
    public class GetAllPlayerDataQuery : IRequest<IEnumerable<ElementDto>>
    {
    }

    public class GetAllPlayerDataQueryHandler : IRequestHandler<GetAllPlayerDataQuery, IEnumerable<ElementDto>>
    {
        private readonly IElementDataService _elementDataService;
        private readonly IMapper _mapper;

        public GetAllPlayerDataQueryHandler(
            IElementDataService elementDataService,
            IMapper mapper)
        {
            _elementDataService = elementDataService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ElementDto>> Handle(GetAllPlayerDataQuery request, CancellationToken cancellationToken)
        {
            var playerEntities = await _elementDataService._elementsCollection.Find(_ => true).ToListAsync();
            var playerData = _mapper.Map<IEnumerable<Element>, IEnumerable<ElementDto>>(playerEntities);
            return playerData;
        }
    }
}
