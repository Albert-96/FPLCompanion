using AutoMapper;
using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using FPLCompanion.Dto;
using Newtonsoft.Json;

namespace FPLCompanion.ApplicationServices.Requests.General.Commands
{

    public class ImportFplData
    {
        private readonly IElementRepository _elementRepository;
        private readonly IElementTypeRepository _elementTypeRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IFixtureRepository _fixtureRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IElementDetailRepository _elementDetailRepository;
        private readonly IDreamTeamRepository _dreamTeamRepository;
        private readonly IEventElementRepository _eventElementRepository;
        private readonly IMapper _mapper;

        public ImportFplData(
            ITeamRepository teamRepository,
            IElementRepository elementRepository,
            IElementTypeRepository elementTypeRepository,
            IFixtureRepository fixtureRepository,
            IEventRepository eventRepository,
            IElementDetailRepository elementDetailRepository,
            IDreamTeamRepository dreamTeamRepository,
            IEventElementRepository eventElementRepository,
            IMapper mapper)
        {
            _elementRepository = elementRepository;
            _elementTypeRepository = elementTypeRepository;
            _teamRepository = teamRepository;
            _fixtureRepository = fixtureRepository;
            _eventRepository = eventRepository;
            _elementDetailRepository = elementDetailRepository;
            _dreamTeamRepository = dreamTeamRepository;
            _eventElementRepository = eventElementRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();

                var response = await client.GetAsync(FPLConstants.FplGeneralApi);
                GeneralInfoDto deserializedGeneralInfo = JsonConvert.DeserializeObject<GeneralInfoDto>(await response.Content.ReadAsStringAsync());
                var teams = _mapper.Map<IEnumerable<TeamDto>, IEnumerable<Team>>(deserializedGeneralInfo.teams).ToList();
                var events = _mapper.Map<IEnumerable<EventDto>, IEnumerable<Event>>(deserializedGeneralInfo.events).ToList();
                var elementTypes = _mapper.Map<IEnumerable<ElementTypeDto>, IEnumerable<ElementType>>(deserializedGeneralInfo.element_types).ToList();
                var players = _mapper.Map<IEnumerable<ElementDto>, IEnumerable<Element>>(deserializedGeneralInfo.elements).ToList();
                players = players.Select(x =>
                {
                    x.teamInfo = teams.FirstOrDefault(p => p.id == x.team).short_name;
                    x.elementTypeInfo = elementTypes.FirstOrDefault(p => p.id == x.element_type).singular_name_short;
                    x.current_cost = (float)(x.now_cost ?? 0) / (float)10;
                    return x;
                }).ToList();
                var elementTask = _elementRepository.UpdateMany(players);
                var teamTask = _teamRepository.UpdateMany(teams);
                var elementTypeTask = _elementTypeRepository.UpdateMany(elementTypes);
                var eventTask = _eventRepository.UpdateMany(events);

                client.DefaultRequestHeaders.Accept.Clear();
                response = await client.GetAsync(FPLConstants.FplFixturesApi);
                var deserializedFixture = JsonConvert.DeserializeObject<List<FixtureDto>>(await response.Content.ReadAsStringAsync());
                var fixtures = _mapper.Map<IEnumerable<FixtureDto>, IEnumerable<Fixture>>(deserializedFixture).ToList();
                var fixtureTask = _fixtureRepository.UpdateMany(fixtures);

                List<DreamTeam> dreamTeams = new List<DreamTeam>();
                foreach (var @event in events)
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    response = await client.GetAsync($"{FPLConstants.FplDreamTeamApi}{@event.id}");
                    var deserializedDreamTeam = JsonConvert.DeserializeObject<DreamTeamDto>(await response.Content.ReadAsStringAsync());
                    var dreamTeam = _mapper.Map<DreamTeamDto, DreamTeam>(deserializedDreamTeam);
                    dreamTeam.id = @event.id;
                    dreamTeams.Add(dreamTeam);
                }

                var dreamTeamTask = _dreamTeamRepository.UpdateMany(dreamTeams);

                List<ElementDetail> playerDetails = new List<ElementDetail>();
                foreach (var player in players)
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    response = await client.GetAsync($"{FPLConstants.FplElementApi}{player.id}");
                    var deserializedPlayerDetail = JsonConvert.DeserializeObject<ElementDetailDto>(await response.Content.ReadAsStringAsync());
                    var playerDetail = _mapper.Map<ElementDetailDto, ElementDetail>(deserializedPlayerDetail);
                    playerDetail.id = player.id;
                    playerDetails.Add(playerDetail);
                }

                var playerDetailTask = _elementDetailRepository.UpdateMany(playerDetails);

                List<EventElement> eventDetails = new List<EventElement>();
                foreach (var @event in events)
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    var url = String.Format(FPLConstants.FplEventLiveApi, @event.id);
                    response = await client.GetAsync(url);
                    var deserializedEventDetail = JsonConvert.DeserializeObject<EventDetailDto>(await response.Content.ReadAsStringAsync());
                    var eventDetail = _mapper.Map<EventDetailDto, EventDetail>(deserializedEventDetail);
                    eventDetail.id = @event.id;
                    var eventElements = eventDetail.elements
                        .Where(x => x.stats.minutes > 0)
                        .Select(x =>
                        {
                            x.eventId = @event.id;
                            return x;
                        })
                        .ToList();
                    eventDetails.AddRange(eventElements);
                }

                var eventDetailTask = _eventElementRepository.UpdateMany(eventDetails);

                Task.WaitAll(elementTask, teamTask, elementTask, fixtureTask, eventTask, playerDetailTask, dreamTeamTask);
                await eventDetailTask;

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
