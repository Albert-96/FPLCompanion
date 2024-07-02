using FPLCompanion.Data.Entities;
using PrimeNGTableExtension.Models;
namespace FPLCompanion.DataService.Abstractions
{
    public interface IElementRepository : IEntityRepository<Element>
    {
        IQueryable<Element> GetGridData(TableRequestModel request);
        int GetGridCount(TableRequestModel request);
        Task UpdateMany(List<Element> elements);
    }
}
