using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FPLCompanion.DataService.Services
{
    public class ElementDataService: IElementDataService
    {
        public IMongoCollection<Element> _elementsCollection { get; set; }

        public ElementDataService(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _elementsCollection = mongoDatabase.GetCollection<Element>(DatabaseTables.Element);
        }

        public async Task CreateAsync(Element newBook) =>
            await _elementsCollection.InsertOneAsync(newBook);

        public async Task InsertMany(List<Element> elements) =>
            await _elementsCollection.InsertManyAsync(elements);

        public async Task UpdateAsync(int id, Element updatedBook)
        {
            var element = await _elementsCollection.Find(x => x.id == id).FirstOrDefaultAsync();
            if (element != null)
            {
                updatedBook._Id = element._Id;
            }

            await _elementsCollection.ReplaceOneAsync(x => x.id == id, updatedBook, new UpdateOptions { IsUpsert = true });
        }

        public async Task RemoveAsync(string id) =>
        await _elementsCollection.DeleteOneAsync(x => x._Id == id);
    }
}
