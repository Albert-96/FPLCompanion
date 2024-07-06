using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using Microsoft.Extensions.Options;

namespace FPLCompanion.DataService.Services
{
    public class ElementDetailRepository(
        ApplicationDbContext context,
        IOptions<DatabaseSettings> databaseSettings) : EntityRepository<ElementDetail>(context, databaseSettings, "ElementDetail"), IElementDetailRepository
    {
        public async Task UpdateMany(List<ElementDetail> elementDetails)
        {
            for (int i = 0; i < elementDetails.Count; i++)
            {
                await UpdateMongoAsync(x => x.id == elementDetails[i].id, elementDetails[i]);
            }
        }
    }
}
