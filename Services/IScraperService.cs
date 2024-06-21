using System.Collections.Generic;
using System.Threading.Tasks;
using ScraperApi.Models;

public interface IScraperService
{
    Task<IEnumerable<Actor>> GetTopActorsAsync();
}