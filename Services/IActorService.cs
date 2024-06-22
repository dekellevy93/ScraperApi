using ScraperApi.Models;

namespace ScraperApi.Services
{
    public interface IActorService
    {
        Task<List<ActorDto>> GetAllActorsAsync(string nameFilter = null,
                                       int? minRank = null,
                                       int? maxRank = null,
                                       int? pageNumber = 1,
                                       int? pageSize = 10);
        Task<ActorDto> GetActorByIdAsync(int id);
        Task AddActorAsync(Actor actor);
        Task UpdateActorAsync(Actor actor);
        Task DeleteActorAsync(int id);
    }
}
