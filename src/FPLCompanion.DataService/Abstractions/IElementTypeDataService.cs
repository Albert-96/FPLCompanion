using FPLCompanion.Data.Entities;
using MongoDB.Driver;

namespace FPLCompanion.DataService.Abstractions
{
    public interface IElementTypeDataService
    {
        IMongoCollection<ElementType> _elementTypeCollection { get; set; }

        Task UpdateAsync(int id, ElementType player);

        Task UpdateMany(List<ElementType> elements);

        Task CreateAsync(ElementType player);

        Task InsertMany(List<ElementType> players);

        Task RemoveAsync(int id);
    }
}
