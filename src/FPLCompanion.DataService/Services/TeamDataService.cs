using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLCompanion.DataService.Services
{
    public class TeamDataService : ITeamDataService
    {
        public IMongoCollection<Team> _teamsCollection { get; set; }

        public TeamDataService(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _teamsCollection = mongoDatabase.GetCollection<Team>(DatabaseTables.Team);
        }

        public async Task CreateAsync(Team newBook) =>
            await _teamsCollection.InsertOneAsync(newBook);

        public async Task InsertMany(List<Team> elements) =>
            await _teamsCollection.InsertManyAsync(elements);

        public async Task UpdateAsync(int id, Team updatedBook)
        {
            var element = await _teamsCollection.Find(x => x.id == id).FirstOrDefaultAsync();
            if (element != null)
            {
                updatedBook._Id = element._Id;
            }

            await _teamsCollection.ReplaceOneAsync(x => x.id == id, updatedBook, new UpdateOptions { IsUpsert = true });
        }

        public async Task UpdateMany(List<Team> elements)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                var element = elements[i];
                await UpdateAsync(element.id, element);
            }
        }

        public async Task RemoveAsync(string id) =>
            await _teamsCollection.DeleteOneAsync(x => x._Id == id);
    }
}
