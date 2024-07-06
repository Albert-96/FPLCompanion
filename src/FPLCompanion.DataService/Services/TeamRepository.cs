using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using Microsoft.Extensions.Options;
namespace FPLCompanion.DataService.Services
{
    public class TeamRepository(
        ApplicationDbContext context,
        IOptions<DatabaseSettings> databaseSettings) : EntityRepository<Team>(context, databaseSettings, "Team"), ITeamRepository
    {
        public async Task UpdateMany(List<Team> teams)
        {
            for (int i = 0; i < teams.Count; i++)
            {
                await UpdateMongoAsync(x => x.id == teams[i].id, teams[i]);
            }
        }
    }
}
