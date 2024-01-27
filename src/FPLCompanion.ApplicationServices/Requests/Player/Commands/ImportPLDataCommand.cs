using AutoMapper;
using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using FPLCompanion.Dto;
using MediatR;
using Newtonsoft.Json;

namespace FPLCompanion.ApplicationServices.Requests.Player.Commands
{
    public class ImportPLDataCommand : IRequest<int>
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
                var response = await client.GetAsync("https://fantasy.premierleague.com/api/bootstrap-static/");
                RootDto deserializedClass = JsonConvert.DeserializeObject<RootDto>(await response.Content.ReadAsStringAsync(cancellationToken));

                var players = _mapper.Map<IEnumerable<ElementDto>, IEnumerable<Element>>(deserializedClass.elements).ToList();
                await _elementDataService.UpdateMany(players);

                var teams = _mapper.Map<IEnumerable<TeamDto>, IEnumerable<Team>>(deserializedClass.teams).ToList();
                await _teamDataService.UpdateMany(teams);

                var elementTypes = _mapper.Map<IEnumerable<ElementTypeDto>, IEnumerable<ElementType>>(deserializedClass.element_types).ToList();
                await _elemenTypeDataService.UpdateMany(elementTypes);

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
