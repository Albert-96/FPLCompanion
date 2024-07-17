using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using Microsoft.Extensions.Options;

namespace FPLCompanion.DataService.Services
{
    public class EventElementRepository(
        ApplicationDbContext context,
        IOptions<DatabaseSettings> databaseSettings) : EntityRepository<EventElement>(context, databaseSettings, "EventElement"), IEventElementRepository
    {
        public async Task UpdateMany(List<EventElement> dreamTeams)
        {
            for (int i = 0; i < dreamTeams.Count; i++)
            {
                await UpdateMongoAsync(
                    x => x.elementId == dreamTeams[i].elementId && x.eventId == dreamTeams[i].eventId,
                    dreamTeams[i]);
            }
        }
    }
}
