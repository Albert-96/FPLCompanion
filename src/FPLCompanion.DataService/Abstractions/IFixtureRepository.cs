using FPLCompanion.Data.Entities;

namespace FPLCompanion.DataService.Abstractions
{
    public interface IFixtureRepository : IEntityRepository<Fixture>
    {
        Task UpdateMany(List<Fixture> fixtures);
    }
}
