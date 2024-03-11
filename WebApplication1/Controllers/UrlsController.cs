using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Constants;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

public class UrlsController : Controller
{
    private UrlRepository _urlRepository = UrlRepository.GetRepository();
    private UrlShortenerService _service = UrlShortenerService.GetService();
    
    public IActionResult Index()
    {
        return Redirect(UrlConstants.AngularServerLink);
    }

    public IActionResult GetUrls()
    {
        return new JsonResult(_urlRepository.GetUrls());
    }

    public IActionResult GetUrl(int id)
    {
        Url url = _urlRepository.GetUrl(id);
        if (url != null)
        {
            return new JsonResult(url);
        }
        return BadRequest();
    }

    [HttpDelete]
    public IActionResult DeleteUrl(int id)
    {
        try
        {
            _urlRepository.DeleteUrl(id);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }

    [HttpPost]
    public IActionResult AddUrl()
    {
        string text;
        using (StreamReader reader = new StreamReader(HttpContext.Request.Body)) {
            text = reader.ReadToEndAsync().Result;
        }

        string fullUrl = JsonObject.Parse(text)[UrlConstants.JsonFullUrl].ToString();
        string creator = JsonObject.Parse(text)[UrlConstants.JsonCreator].ToString();
        DateTime creationDate = new DateTime(1970, 1, 1)
            .AddMilliseconds(Int64.Parse(JsonObject.Parse(text)[UrlConstants.JsonCreationDate].ToString()))
            .ToLocalTime();
        
        if (!Uri.TryCreate(fullUrl, UriKind.Absolute, out _))
        {
            return BadRequest("url is invalid");
        }

        if (_urlRepository.HasFullUrl(fullUrl))
        {
            return BadRequest("such url already exists");
        }

        string shortUrl = _service.GetShortenedUrl();
        
        _urlRepository.AddUrl(new Url(fullUrl, shortUrl, creator, creationDate));
        return Ok();
    }

    [Route("{shortUrl:length(10)}")]
    public IActionResult RedirectToFullUrl(string shortUrl)
    {
        return Redirect(_urlRepository.GetByShortUrl(shortUrl).FullUrl);
    }
}