using FPLCompanion.Data.Entities;

namespace FPLCompanion.DataService.Abstractions
{
    public interface IEventRepository
    {
        Task UpdateMany(List<Event> events);
    }
}
