using ScraperApi.Data.Repositories;
using ScraperApi.Models;

namespace ScraperApi.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _repository;

        public ActorService(IActorRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ActorDto> GetActorByIdAsync(int id)
        {
            return await _repository.GetActorByIdAsync(id);
        }

        public async Task AddActorAsync(Actor actor)
        {
            await _repository.AddActorAsync(actor);
        }

        public async Task UpdateActorAsync(Actor actor)
        {
            await _repository.UpdateActorAsync(actor);
        }

        public async Task DeleteActorAsync(int id)
        {
            await _repository.DeleteActorAsync(id);
        }

        public async Task<List<ActorDto>> GetAllActorsAsync(string nameFilter = null, int? minRank = null, int? maxRank = null, int? pageNumber = 1, int? pageSize = 10)
        {
            return await _repository.GetAllActorsAsync(nameFilter , minRank, maxRank, pageNumber,pageSize);
        }
    }
}
