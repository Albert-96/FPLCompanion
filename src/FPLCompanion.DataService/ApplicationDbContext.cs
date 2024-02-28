using FPLCompanion.Data.Entities;
using FPLCompanion.DataService.Abstractions;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace FPLCompanion.DataService
{
    /// <summary>
    /// Application Database Context
    /// </summary>
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        /// <summary>
        /// Events entity.
        /// </summary>
        public DbSet<Element> Elements { get; set; }

        /// <summary>
        /// EF Model Creator Function. 
        /// </summary>
        /// <param name="modelBuilder">EF Model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Element>(e =>
            {
                e.ToCollection("Element");
                e.Property(p => p.id).HasElementName("_id");
            });
        }
    }
}
