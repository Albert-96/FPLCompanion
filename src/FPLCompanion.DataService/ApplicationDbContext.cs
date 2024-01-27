using FPLCompanion.Data.Entities;
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
        public Element Events { get; set; }

        /// <summary>
        /// Teams entity.
        /// </summary>
        public Team Teams { get; set; }

        /// <summary>
        /// Element type entity.
        /// </summary>
        public ElementType ElementTypes { get; set; }

        /// <summary>
        /// EF Model Creator Function. 
        /// </summary>
        /// <param name="modelBuilder">EF Model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Element>().ToCollection("events");
            modelBuilder.Entity<Team>().ToCollection("teams");
            modelBuilder.Entity<ElementType>().ToCollection("elementTypes");
        }
    }
}
