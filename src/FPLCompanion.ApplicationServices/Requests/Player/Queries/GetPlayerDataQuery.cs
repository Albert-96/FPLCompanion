using AutoMapper;
using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using FPLCompanion.Dto.ViewModels;
using MediatR;

namespace FPLCompanion.ApplicationServices.Requests.Player.Queries
{
    public class GetPlayerDataQuery : IRequest<PlayerDetailViewDto>
    {
        public int elementId { get; set; }
    }

    public class GetPlayerDataQueryHandler : IRequestHandler<GetPlayerDataQuery, PlayerDetailViewDto>
    {
        private readonly IMapper Mapper;
        private IElementRepository ElementRepository;
        private IElementDetailRepository ElementDetailRepository;
        private ITeamRepository TeamRepository { get; set; }

        public GetPlayerDataQueryHandler(
            ITeamRepository teamRepository,
            IElementRepository elementRepository,
            IElementDetailRepository elementDetailRepository,
            IMapper mapper)
        {
            Mapper = mapper;
            ElementRepository = elementRepository;
            ElementDetailRepository = elementDetailRepository;
            TeamRepository = teamRepository;
        }

        public async Task<PlayerDetailViewDto> Handle(GetPlayerDataQuery request, CancellationToken cancellationToken)
        {
            var result = new PlayerDetailViewDto();

            try
            {
                var player = await ElementRepository.GetByIdAsync(request.elementId);
                var teams = Mapper.Map<List<Team>, List<TeamViewModelDto>>(await TeamRepository.GetAllAsync());
                var playerDetail = await ElementDetailRepository.GetByIdAsync(request.elementId);
                result = Mapper.Map<Element, PlayerDetailViewDto>(player);
                result.teamDetail = teams.FirstOrDefault(x => x.id == player.team);
                result.elementFixtures = playerDetail.fixtures.Select(x =>
                {
                    var y = Mapper.Map<ElementFixture, ElementFixtureViewModelDto>(x);
                    y.home = teams.FirstOrDefault(p => p.id == x.team_h);
                    y.away = teams.FirstOrDefault(p => p.id == x.team_a);
                    return y;
                }).ToList();
                result.previousFixtures = playerDetail.history.Select(x =>
                {
                    var y = Mapper.Map<History, HistoryViewDto>(x);
                    y.opponent_team = teams.FirstOrDefault(p => p.id == x.opponent_team);
                    return y;
                }).ToList();
                result.previousSeasons = Mapper.Map<List<HistoryPast>, List<HistoryPastViewDto>>(playerDetail.history_past);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}
