using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ScraperApi.Models;

public class ImdbScraperService : IScraperService
{
    private const string ImdbUrl = "https://www.imdb.com/list/ls054840033/";

    public async Task<IEnumerable<Actor>> GetTopActorsAsync()
    {
        var actors = new List<Actor>();
        var httpClient = new HttpClient();

        var html = await httpClient.GetStringAsync(ImdbUrl);
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var actorNodes = doc.DocumentNode.SelectNodes("//div[@class='lister-item-content']/h3/a");

        if (actorNodes != null)
        {
            foreach (var actorNode in actorNodes)
            {
                var actorName = actorNode.InnerText.Trim();
                actors.Add(new Actor { Name = actorName });
            }
        }

        return actors;
    }
}