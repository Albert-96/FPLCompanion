using FPLCompanion.Data.Entities;

namespace FPLCompanion.DataService.Abstractions
{
    public interface IFixtureRepository
    {
        Task UpdateMany(List<Fixture> fixtures);
    }
}
