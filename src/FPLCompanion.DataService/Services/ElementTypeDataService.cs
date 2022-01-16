using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FPLCompanion.DataService.Services
{
    public class ElementTypeDataService : IElementTypeDataService
    {
        public IMongoCollection<ElementType> _elementTypeCollection { get; set; }

        public ElementTypeDataService(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _elementTypeCollection = mongoDatabase.GetCollection<ElementType>(DatabaseTables.ElementType);
        }

        public async Task CreateAsync(ElementType newBook) =>
            await _elementTypeCollection.InsertOneAsync(newBook);

        public async Task InsertMany(List<ElementType> elements) =>
            await _elementTypeCollection.InsertManyAsync(elements);

        public async Task UpdateAsync(int id, ElementType updatedBook)
        {
            var element = await _elementTypeCollection.Find(x => x.id == id).FirstOrDefaultAsync();
            if (element != null)
            {
                updatedBook._Id = element._Id;
            }

            await _elementTypeCollection.ReplaceOneAsync(x => x.id == id, updatedBook, new UpdateOptions { IsUpsert = true });
        }

        public async Task UpdateMany(List<ElementType> elements)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                var element = elements[i];
                await UpdateAsync(element.id, element);
            }
        }

        public async Task RemoveAsync(string id) =>
            await _elementTypeCollection.DeleteOneAsync(x => x._Id == id);
    }
}
