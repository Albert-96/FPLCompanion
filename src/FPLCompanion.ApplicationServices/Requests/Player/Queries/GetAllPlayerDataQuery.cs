using AutoMapper;
using FPLCompanion.Data.Entities;
using FPLCompanion.DataService;
using FPLCompanion.Dto;
using MediatR;
using MongoDB.Driver;
using PrimeNGTableExtension;
using PrimeNGTableExtension.Models;
using FPLCompanion.DataService.Abstractions;
using FPLCompanion.DataService.Services;

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
        private readonly IElementRepository _elementRepository;

        public GetAllPlayerDataQueryHandler(
            ApplicationDbContext context,
            IElementRepository elementRepository,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _elementRepository = elementRepository;
        }

        public async Task<TableResponseModel<ElementDto>> Handle(GetAllPlayerDataQuery request, CancellationToken cancellationToken)
        {
            var result = new TableResponseModel<ElementDto>();

            try
            {
                var playerEntities = _elementRepository.GetGridData(request.gridParams);
                result.Records = _mapper.Map<IEnumerable<Element>, IEnumerable<ElementDto>>(playerEntities);
                result.TotalRecords = _elementRepository.GetGridCount(request.gridParams);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}
