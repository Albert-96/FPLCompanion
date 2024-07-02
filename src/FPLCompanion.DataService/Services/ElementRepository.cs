using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using Microsoft.Extensions.Options;
using PrimeNGTableExtension;
using PrimeNGTableExtension.Models;

namespace FPLCompanion.DataService.Services
{
    public class ElementRepository(
        ApplicationDbContext context,
        IOptions<DatabaseSettings> databaseSettings) : EntityRepository<Element>(context, databaseSettings, "Element"), IElementRepository
    {
        public IQueryable<Element> GetGridData(TableRequestModel request)
        {
            return _context.Elements
                    .PrimeNGTableQuery(request)
                    .Select(x => x);
        }

        public int GetGridCount(TableRequestModel request)
        {
            return _context.Elements.PrimeNGTableCount(request);
        }

        public async Task UpdateMany(List<Element> elements)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                await UpdateMongoAsync(x => x.id == elements[i].id, elements[i]);
            }
        }
    }
}
