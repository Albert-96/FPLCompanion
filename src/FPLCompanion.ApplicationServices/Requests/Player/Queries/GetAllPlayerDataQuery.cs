using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using FPLCompanion.Data.Entities;
using FPLCompanion.DataService;
using FPLCompanion.Dependencies;
using FPLCompanion.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Xml;

namespace FPLCompanion.ApplicationServices.Requests.Player.Queries
{
    public class GetAllPlayerDataQuery : IRequest<GridResponseDto<ElementDto>>
    {
        public GridDto gridParams { get; set; }
    }

    public class GetAllPlayerDataQueryHandler : IRequestHandler<GetAllPlayerDataQuery, GridResponseDto<ElementDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllPlayerDataQueryHandler(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GridResponseDto<ElementDto>> Handle(GetAllPlayerDataQuery request, CancellationToken cancellationToken)
        {
            var result = new GridResponseDto<ElementDto>();

            try
            {
                var playerEntities = _context.Elements
                    .OrderBy(p => p.web_name)
                    .Skip(request.gridParams.First ?? 0)
                    .Take(request.gridParams.Rows ?? 15)
                    .Select(x => x);
                result.records = _mapper.Map<IEnumerable<Element>, IEnumerable<ElementDto>>(playerEntities);
                result.totalRecords = _context.Elements.Count();
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}
