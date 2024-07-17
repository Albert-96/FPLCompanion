using FPLCompanion.Data.Entities;
namespace FPLCompanion.DataService.Abstractions
{
    public interface IEventElementRepository : IEntityRepository<EventElement>
    {
        Task UpdateMany(List<EventElement> elements);
    }
}
