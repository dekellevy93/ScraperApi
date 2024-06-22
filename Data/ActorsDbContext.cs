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
            // Ensure unique Rank constraint
            modelBuilder.Entity<Actor>()
                .HasIndex(a => a.Rank)
                .IsUnique();
        }
    }
}
