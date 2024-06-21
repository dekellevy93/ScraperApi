using Microsoft.EntityFrameworkCore;
using ScraperApi.Models;

namespace ScraperApi.Data
{
    public class ActorsDbContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }

        public ActorsDbContext(DbContextOptions<ActorsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasIndex(e => e.Rank).IsUnique();
            });
        }
    }
}
