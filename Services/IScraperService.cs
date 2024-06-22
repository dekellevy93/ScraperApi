using ScraperApi.Models;

public interface IScraperService
{
    Task<IEnumerable<Actor>> GetTopActorsAsync();
    Task LoadActorsIntoDatabaseAsync(IEnumerable<Actor> actors);
}