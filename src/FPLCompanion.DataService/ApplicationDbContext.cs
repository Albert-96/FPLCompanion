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
        /// Elements entity.
        /// </summary>
        public DbSet<Element> Elements { get; set; }

        /// <summary>
        /// Teams entity.
        /// </summary>
        public DbSet<Team> Teams { get; set; }

        /// <summary>
        /// Element Types entity.
        /// </summary>
        public DbSet<ElementType> ElementTypes { get; set; }

        /// <summary>
        /// Fixtures entity.
        /// </summary>
        public DbSet<Fixture> Fixtures { get; set; }

        /// <summary>
        /// Events entity.
        /// </summary>
        public DbSet<Event> Events { get; set; }

        /// <summary>
        /// Element Details entity.
        /// </summary>
        public DbSet<ElementDetail> ElementDetails { get; set; }

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
            modelBuilder.Entity<Team>(e =>
            {
                e.ToCollection("Team");
                e.Property(p => p.id).HasElementName("_id");
            });
            modelBuilder.Entity<ElementType>(e =>
            {
                e.ToCollection("ElementType");
                e.Property(p => p.id).HasElementName("_id");
            });
            modelBuilder.Entity<Fixture>(e =>
            {
                e.ToCollection("Fixture");
                e.Property(p => p.id).HasElementName("_id");
            });
            modelBuilder.Entity<Event>(e =>
            {
                e.ToCollection("Event");
                e.Property(p => p.id).HasElementName("_id");
            });
            modelBuilder.Entity<ElementDetail>(e =>
            {
                e.ToCollection("ElementDetail");
                e.Property(p => p.id).HasElementName("_id");
            });
        }
    }
}
