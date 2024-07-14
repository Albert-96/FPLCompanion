using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using Microsoft.Extensions.Options;

namespace FPLCompanion.DataService.Services
{
    public class DreamTeamRepository(
        ApplicationDbContext context,
        IOptions<DatabaseSettings> databaseSettings) : EntityRepository<DreamTeam>(context, databaseSettings, "DreamTeam"), IDreamTeamRepository
    {
        public async Task UpdateMany(List<DreamTeam> dreamTeams)
        {
            for (int i = 0; i < dreamTeams.Count; i++)
            {
                await UpdateMongoAsync(x => x.id == dreamTeams[i].id, dreamTeams[i]);
            }
        }
    }
}
