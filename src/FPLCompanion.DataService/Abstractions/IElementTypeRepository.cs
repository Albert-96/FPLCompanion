using FPLCompanion.Data.Entities;

namespace FPLCompanion.DataService.Abstractions
{
    public interface IElementTypeRepository : IEntityRepository<ElementType>
    {
        Task UpdateMany(List<ElementType> elementTypes);
    }
}
