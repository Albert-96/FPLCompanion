using AutoMapper;
using FPLCompanion.Data.Entities;
using FPLCompanion.Data.ViewModels;
using FPLCompanion.DataService.Abstractions;
using MediatR;
using Newtonsoft.Json;

namespace FPLCompanion.ApplicationServices.Requests.Player.Commands
{
    public class ImportPlayerDataCommand : IRequest<int>
    {
    }

    public class ImportPlayerDataCommandHandler : IRequestHandler<ImportPlayerDataCommand, int>
    {
        private readonly IElementDataService _elementDataService;
        private readonly IMapper _mapper;

        public ImportPlayerDataCommandHandler(
            IElementDataService elementDataService,
            IMapper mapper)
        {
            _elementDataService = elementDataService;
            _mapper = mapper;
        }

        public async Task<int> Handle(ImportPlayerDataCommand request, CancellationToken cancellationToken)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetAsync("https://fantasy.premierleague.com/api/bootstrap-static/");
                RootDto deserializedClass = JsonConvert.DeserializeObject<RootDto>(await response.Content.ReadAsStringAsync());
                var players = _mapper.Map<IEnumerable<ElementDto>, IEnumerable<Element>>(deserializedClass.elements).ToList();
                await _elementDataService.InsertMany(players);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
