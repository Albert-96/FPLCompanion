using AutoMapper;
using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using FPLCompanion.Dto.ViewModels;
using MediatR;

namespace FPLCompanion.ApplicationServices.Requests.Fixture.Queries
{
    public class GetEventFixtureQuery : IRequest<List<FixtureViewModelDto>>
    {
        public int eventId { get; set; }
    }

    public class GetEventFixtureQueryHandler : IRequestHandler<GetEventFixtureQuery, List<FixtureViewModelDto>>
    {
        private readonly IMapper Mapper;
        private IFixtureRepository FixtureRepository;
        private ITeamRepository TeamRepository;

        public GetEventFixtureQueryHandler(
            IFixtureRepository fixtureRepository,
            ITeamRepository teamRepository,
            IMapper mapper)
        {
            Mapper = mapper;
            FixtureRepository = fixtureRepository;
            TeamRepository = teamRepository;
        }

        public async Task<List<FixtureViewModelDto>> Handle(GetEventFixtureQuery request, CancellationToken cancellationToken)
        {
            var result = new List<FixtureViewModelDto>();

            try
            {
                var fixtures = await FixtureRepository.GetFilterAsync(x => x.@event == request.eventId);
                var teams = Mapper.Map<List<Team>, List<TeamViewModelDto>>(await TeamRepository.GetAllAsync());
                //result = Mapper.Map<List<FPLCompanion.Data.Entities.Fixture>, List<FitxtureViewModelDto>>(fixtures);
                result = fixtures.Select(x =>
                {
                    var y = Mapper.Map<FPLCompanion.Data.Entities.Fixture, FixtureViewModelDto>(x);
                    y.home = teams.FirstOrDefault(p => p.id == x.team_h);
                    y.away = teams.FirstOrDefault(p => p.id == x.team_a);
                    return y;
                }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}
