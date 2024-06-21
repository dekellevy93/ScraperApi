using Microsoft.EntityFrameworkCore;
using ScraperApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScraperApi.Data.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly ActorsDbContext _context;

        public ActorRepository(ActorsDbContext context)
        {
            _context = context;
        }

        public async Task<List<ActorDto>> GetAllActorsAsync(string nameFilter, int? minRank, int? maxRank, int pageNumber, int pageSize)
        {
            IQueryable<Actor> query = _context.Actors;

            // Filtering
            if (!string.IsNullOrEmpty(nameFilter))
            {
                query = query.Where(a => a.Name.Contains(nameFilter));
            }
            if (minRank.HasValue)
            {
                query = query.Where(a => a.Rank >= minRank.Value);
            }
            if (maxRank.HasValue)
            {
                query = query.Where(a => a.Rank <= maxRank.Value);
            }

            // Pagination
            var actors = await query
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize)
                   .Select(a => new ActorDto
                   {
                       Name = a.Name,
                       Id = a.Id
                   })
                   .ToListAsync();

            return actors;
        }

        public async Task<Actor> GetActorByIdAsync(int id)
        {
            return await _context.Actors.FindAsync(id);
        }

        public async Task AddActorAsync(Actor actor)
        {
            if (actor == null)
            {
                throw new ArgumentNullException(nameof(actor));
            }

            // Check for duplicate rank
            if (await _context.Actors.AnyAsync(a => a.Rank == actor.Rank))
            {
                throw new InvalidOperationException("An actor with the same rank already exists.");
            }

            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateActorAsync(Actor actor)
        {
            if (actor == null)
            {
                throw new ArgumentNullException(nameof(actor));
            }

            // Check for duplicate rank (assuming Rank is a property of Actor)
            // This assumes Rank is a unique property. 
            // If not, you might need to add a unique constraint to the database.
            if (await _context.Actors.AnyAsync(a => a.Id != actor.Id && a.Rank == actor.Rank))
            {
                throw new InvalidOperationException("An actor with the same rank already exists.");
            }

            try
            {
                // Use Update instead of manually setting the state for better performance
                _context.Actors.Update(actor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency issues, e.g., another user updated the same actor
                // Log the exception, provide a user-friendly message, and potentially offer options like reloading the data.
                // For this example, we'll simply rethrow the exception for now.
                throw new Exception("The actor was updated by another user. Please refresh and try again.", ex);
            }
            catch (DbUpdateException ex)
            {
                // Handle general database update errors (e.g., constraint violations)
                // Log the error and provide a user-friendly message
                throw new Exception("An error occurred while updating the actor. Please check your data and try again.", ex);
            }
        }

        public async Task DeleteActorAsync(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor != null)
            {
                _context.Actors.Remove(actor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
