using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace FPLCompanion.DataService.Services
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        public IMongoCollection<T> _collection { get; set; }
        protected ApplicationDbContext _context;
        protected DbSet<T> table;

        public EntityRepository(
            ApplicationDbContext context,
            IOptions<DatabaseSettings> databaseSettings,
            string tableName)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _collection = mongoDatabase.GetCollection<T>(tableName);
            _context = context;
            table = _context.Set<T>();
        }

        public async ValueTask<T> GetByIdAsync(object id)
        {
            return await table.FindAsync(id);
        }

        public async ValueTask<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task InsertAsync(T insertRecord)
        {
            await table.AddAsync(insertRecord);
        }

        public async Task UpdateMongoAsync(Expression<Func<T, bool>> expression, T updatedRecord)
        {
            await _collection.ReplaceOneAsync(expression, updatedRecord, new ReplaceOptions { IsUpsert = true });
        }

        public async Task DeleteAsync(object id)
        {
            var element = await table.FindAsync(id);
            if (element != null)
            {
                table.Remove(element);
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
