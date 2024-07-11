using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortner.Models;

namespace UrlShortner.Controllers;


[Authorize(AuthenticationSchemes = $"{JwtBearerDefaults.AuthenticationScheme},BasicAuthentication")]
[ApiController]
[Route("[controller]")]
public class ShortUrlsController : ControllerBase
{
    private readonly Ilogger<ShortUrlsController> _logger;
    private readonly UrlShortenerContext _context;

    public ShortUrlsController(ILogger<ShortUrlsController> logger, UrlShortenerContext context)
    {
        _logger = logger;
        _context = context;
    }

    [Authorize(Policy = "AddCustomer")]
    [HttpPut("{id}")]
    public string CreateShortUrl(string id, [FromBody] JsonElement body)
    {
        Console.WriteLine($"request to create: {id}, {body.GetProperty("url")}");
        return "https://shortUrl.com";
    }

    [Authorize(Roles = "Emperor,Deacon")]
    [HttpDelete("{id}")]
    public string DeleteShortUrl(string id)
    {
        Console.WriteLine($"request to delete: {id}");
        return "deleted!";
    }

    [AllowAnonymous]
    [HttpGet("original/url/shortUrl")]
    public string GetOriginalUrl(string id)
    {
        var url = _context.Urls.SingleOrDefault(u => u.ShortenedUrl == shortUrl);

        if (url == null)
        {
            return "shortened URL not found";
        }
        return url.OriginalUrl;
    }

    [HttpGet]
    public List<string> List()
    {
        Console.WriteLine($"request to list");

        return
        [
            "url1",
            "url2"
        ];
    }
}
