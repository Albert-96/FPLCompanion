using FPLCompanion.Data.Entities;

namespace FPLCompanion.DataService.Abstractions
{
    public interface IEventRepository : IEntityRepository<Event>
    {
        Task UpdateMany(List<Event> events);
    }
}
