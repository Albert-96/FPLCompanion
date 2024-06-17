using AutoMapper;
using FPLCompanion.ApplicationServices.Requests.Player.Commands;
using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using FPLCompanion.Dto;
using MediatR;
using Newtonsoft.Json;

namespace FPLCompanion.ApplicationServices.Requests.General.Commands
{
    public class ImportFplData : IRequest<int>
    {
    }

    public class ImportPLDataCommandHandler : IRequestHandler<ImportPLDataCommand, int>
    {
        private readonly IElementDataService _elementDataService;
        private readonly ITeamDataService _teamDataService;
        private readonly IElementTypeDataService _elemenTypeDataService;
        private readonly IMapper _mapper;

        public ImportPLDataCommandHandler(
            IElementDataService elementDataService,
            ITeamDataService teamDataService,
            IElementTypeDataService elemenTypeDataService,
            IMapper mapper)
        {
            _elementDataService = elementDataService;
            _teamDataService = teamDataService;
            _elemenTypeDataService = elemenTypeDataService;
            _mapper = mapper;
        }

        public async Task<int> Handle(ImportPLDataCommand request, CancellationToken cancellationToken)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();

                var response = await client.GetAsync(FPLConstants.FplGeneralApi);
                GeneralInfoDto deserializedGeneralInfo = JsonConvert.DeserializeObject<GeneralInfoDto>(await response.Content.ReadAsStringAsync(cancellationToken));
                var teams = _mapper.Map<IEnumerable<TeamDto>, IEnumerable<Team>>(deserializedGeneralInfo.teams).ToList();
                var elementTypes = _mapper.Map<IEnumerable<ElementTypeDto>, IEnumerable<ElementType>>(deserializedGeneralInfo.element_types).ToList();
                var players = _mapper.Map<IEnumerable<ElementDto>, IEnumerable<Element>>(deserializedGeneralInfo.elements).ToList();
                players = players.Select(x =>
                {
                    x.teamInfo = teams.FirstOrDefault(p => p.id == x.team).short_name;
                    x.elementTypeInfo = elementTypes.FirstOrDefault(p => p.id == x.element_type).singular_name_short;
                    x.current_cost = (float)(x.now_cost ?? 0) / (float)10;
                    return x;
                }).ToList();
                var elementTask = _elementDataService.UpdateMany(players);
                var teamTask = _teamDataService.UpdateMany(teams);
                var elementTypeTask = _elemenTypeDataService.UpdateMany(elementTypes);

                client.DefaultRequestHeaders.Accept.Clear();
                response = await client.GetAsync(FPLConstants.FplFixturesApi);
                FixtureDto deserializedFixture = JsonConvert.DeserializeObject<FixtureDto>(await response.Content.ReadAsStringAsync(cancellationToken));

                Task.WaitAll(elementTask, teamTask, elementTask);

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
