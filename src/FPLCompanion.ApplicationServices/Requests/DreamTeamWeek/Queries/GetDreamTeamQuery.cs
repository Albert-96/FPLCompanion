using AutoMapper;
using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using FPLCompanion.Dto;
using MediatR;

namespace FPLCompanion.ApplicationServices.Requests.DreamTeamWeek.Queries
{
    public class GetDreamTeamQuery : IRequest<List<PlayerDto>>
    {
        public int id { get; set; }
    }

    public class GetDreamTeamQueryHandler : IRequestHandler<GetDreamTeamQuery, List<PlayerDto>>
    {
        private readonly IMapper Mapper;
        private IDreamTeamRepository DreamTeamRepository { get; set; }
        private IEventElementRepository EventElementRepository { get; set; }

        public GetDreamTeamQueryHandler(
            IDreamTeamRepository dreamTeamRepository,
            IEventElementRepository eventElementRepository,
            IMapper mapper)
        {
            Mapper = mapper;
            DreamTeamRepository = dreamTeamRepository;
            EventElementRepository = eventElementRepository;
        }

        public async Task<List<PlayerDto>> Handle(GetDreamTeamQuery request, CancellationToken cancellationToken)
        {
            var result = new List<PlayerDto>();

            try
            {
                var dreamTeam = await DreamTeamRepository.GetByIdAsync(request.id);
                var players = await EventElementRepository
                    .GetFilterAsync(x => dreamTeam.team.Any(y => x.elementId == y.element && x.eventId == request.id));
                //result.AddRange(Mapper.Map<List<Element>, List<PlayerDto>>(players));
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}
