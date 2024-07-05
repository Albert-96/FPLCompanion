using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using Microsoft.Extensions.Options;

namespace FPLCompanion.DataService.Services
{
    public class EventRepository(
        ApplicationDbContext context,
        IOptions<DatabaseSettings> databaseSettings) : EntityRepository<Event>(context, databaseSettings, "Event"), IEventRepository
    {
        public async Task UpdateMany(List<Event> events)
        {
            for (int i = 0; i < events.Count; i++)
            {
                await UpdateMongoAsync(x => x.id == events[i].id, events[i]);
            }
        }
    }
}
