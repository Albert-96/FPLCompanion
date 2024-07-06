using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using Microsoft.Extensions.Options;

namespace FPLCompanion.DataService.Services
{
    public class ElementTypeRepository(
        ApplicationDbContext context,
        IOptions<DatabaseSettings> databaseSettings) : EntityRepository<ElementType>(context, databaseSettings, "ElementType"), IElementTypeRepository
    {
        public async Task UpdateMany(List<ElementType> elementTypes)
        {
            for (int i = 0; i < elementTypes.Count; i++)
            {
                await UpdateMongoAsync(x => x.id == elementTypes[i].id, elementTypes[i]);
            }
        }
    }
}
