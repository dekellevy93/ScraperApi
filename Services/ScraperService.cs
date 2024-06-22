using HtmlAgilityPack;
using ScraperApi.Data;
using ScraperApi.Models;

public class ImdbScraperService : IScraperService
{
    private const string ImdbUrl = "https://www.imdb.com/list/ls054840033/";
    private readonly ActorsDbContext _context; 

    public ImdbScraperService(ActorsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Actor>> GetTopActorsAsync()
    {
        var actors = new List<Actor>();
        var httpClient = new HttpClient();

        var html = await httpClient.GetStringAsync(ImdbUrl);
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var actorsList = doc.DocumentNode.SelectNodes("//div[contains(@class, 'dli-parent')]");

        int rank = 1;
        foreach (var actorHtml in actorsList)
        {
            // 1. Extract Actor Name 
            var actorName = actorHtml.SelectSingleNode(".//h3[@class='ipc-title__text']")?.InnerText.Trim().Substring(3);

            // 2. Extract Bio
            var bio = actorHtml.SelectSingleNode(".//div[@data-testid='dli-bio']//div[@class='ipc-html-content-inner-div']")?.InnerText.Trim();

            // 3. Extract Best Movie
            var bestMovie = actorHtml.SelectSingleNode(".//a[@data-testid='nlib-known-for-title']")?.InnerText.Trim();

            // 4. Create Actor Object
            if (actorName != null)
            {
                actors.Add(new Actor
                {
                    Id = rank,
                    Name = actorName,
                    Rank = rank,
                    Bio = bio ?? "",
                    BestMovie = bestMovie ?? ""
                });
            }
            rank++;
        }

        return actors;
    }

    public async Task LoadActorsIntoDatabaseAsync(IEnumerable<Actor> actors)
    {
        if (_context == null)
        {
            throw new InvalidOperationException("Database context is not available.");
        }

        foreach (var actor in actors)
        {
            if (!_context.Actors.Any(a => a.Name == actor.Name))
            {
                _context.Actors.Add(actor);
            }
        }

        await _context.SaveChangesAsync();
    }
}