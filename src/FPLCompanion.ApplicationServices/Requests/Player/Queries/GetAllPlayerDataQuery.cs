using AutoMapper;
using FPLCompanion.Data.Entities;
using FPLCompanion.DataService;
using FPLCompanion.Dto;
using MediatR;
using MongoDB.Driver;
using PrimeNGTableExtension;
using PrimeNGTableExtension.Models;

namespace FPLCompanion.ApplicationServices.Requests.Player.Queries
{
    public class GetAllPlayerDataQuery : IRequest<TableResponseModel<ElementDto>>
    {
        public TableRequestModel gridParams { get; set; }
    }

    public class GetAllPlayerDataQueryHandler : IRequestHandler<GetAllPlayerDataQuery, TableResponseModel<ElementDto>>
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

        public async Task<TableResponseModel<ElementDto>> Handle(GetAllPlayerDataQuery request, CancellationToken cancellationToken)
        {
            var result = new TableResponseModel<ElementDto>();

            try
            {
                var playerEntities = _context.Elements
                    .PrimeNGTableQuery(request.gridParams)
                    .OrderBy(p => p.web_name)
                    .Skip(request.gridParams.First ?? 0)
                    .Take(request.gridParams.Rows ?? 15)
                    .Select(x => x);
                result.Records = _mapper.Map<IEnumerable<Element>, IEnumerable<ElementDto>>(playerEntities);
                result.TotalRecords = _context.Elements.PrimeNGTableCount(request.gridParams);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}
