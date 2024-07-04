using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using Microsoft.Extensions.Options;

namespace FPLCompanion.DataService.Services
{
    public class FixtureRepository(
        ApplicationDbContext context,
        IOptions<DatabaseSettings> databaseSettings) : EntityRepository<Fixture>(context, databaseSettings, "Fixture"), IFixtureRepository
    {
        public async Task UpdateMany(List<Fixture> fixtures)
        {
            for (int i = 0; i < fixtures.Count; i++)
            {
                await UpdateMongoAsync(x => x.id == fixtures[i].id, fixtures[i]);
            }
        }
    }
}
