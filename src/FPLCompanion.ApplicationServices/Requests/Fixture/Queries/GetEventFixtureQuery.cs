using AutoMapper;
using FPLCompanion.DataService.Abstractions;
using FPLCompanion.Dto;
using MediatR;

namespace FPLCompanion.ApplicationServices.Requests.Fixture.Queries
{
    public class GetEventFixtureQuery : IRequest<List<FixtureDto>>
    {
        public int eventId { get; set; }
    }

    public class GetEventFixtureQueryHandler : IRequestHandler<GetEventFixtureQuery, List<FixtureDto>>
    {
        private readonly IMapper Mapper;
        private IFixtureRepository FixtureRepository { get; set; }

        public GetEventFixtureQueryHandler(
            IFixtureRepository fixtureRepository,
            IMapper mapper)
        {
            Mapper = mapper;
            FixtureRepository = fixtureRepository;
        }

        public async Task<List<FixtureDto>> Handle(GetEventFixtureQuery request, CancellationToken cancellationToken)
        {
            var result = new List<FixtureDto>();

            try
            {
                var fixtures = await FixtureRepository.GetFilterAsync(x => x.@event == request.eventId);
                result = Mapper.Map<List<FPLCompanion.Data.Entities.Fixture>, List<FixtureDto>>(fixtures);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}
