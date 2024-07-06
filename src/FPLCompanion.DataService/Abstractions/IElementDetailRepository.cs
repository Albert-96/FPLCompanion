using FPLCompanion.Data.Entities;

namespace FPLCompanion.DataService.Abstractions
{
    public interface IElementDetailRepository : IEntityRepository<ElementDetail>
    {
        Task UpdateMany(List<ElementDetail> elements);
    }
}
