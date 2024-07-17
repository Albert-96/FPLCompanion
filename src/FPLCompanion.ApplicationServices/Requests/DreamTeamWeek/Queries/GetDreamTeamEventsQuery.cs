using AutoMapper;
using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using FPLCompanion.Dto;
using MediatR;

namespace FPLCompanion.ApplicationServices.Requests.DreamTeamWeek.Queries
{
    public class GetDreamTeamEventsQuery : IRequest<List<EventDto>>
    {
    }

    public class GetDreamTeamEventsQueryHandler : IRequestHandler<GetDreamTeamEventsQuery, List<EventDto>>
    {
        private readonly IMapper Mapper;
        private IDreamTeamRepository DreamTeamRepository { get; set; }
        private IEventRepository EventRepository { get; set; }

        public GetDreamTeamEventsQueryHandler(
            IDreamTeamRepository dreamTeamRepository,
            IEventRepository eventRepository,
            IMapper mapper)
        {
            Mapper = mapper;
            DreamTeamRepository = dreamTeamRepository;
            EventRepository = eventRepository;
        }

        public async Task<List<EventDto>> Handle(GetDreamTeamEventsQuery request, CancellationToken cancellationToken)
        {
            var result = new List<EventDto>();

            try
            {
                var dreamTeams = await DreamTeamRepository.GetAllAsync();
                var events = await EventRepository.GetFilterAsync(x => dreamTeams.Any(y => y.id == x.id));
                result = Mapper.Map<List<Event>, List<EventDto>>(events);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}
