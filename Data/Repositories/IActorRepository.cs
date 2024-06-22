using ScraperApi.Models;

namespace ScraperApi.Data.Repositories
{
    public interface IActorRepository
    {
        Task<List<ActorDto>> GetAllActorsAsync(string nameFilter = null,
                                       int? minRank = null,
                                       int? maxRank = null,
                                       int? pageNumber = 0,
                                       int? pageSize = 0);
    Task<ActorDto> GetActorByIdAsync(int id);
        Task AddActorAsync(Actor actor);
        Task UpdateActorAsync(Actor actor);
        Task DeleteActorAsync(int id);
    }
}
