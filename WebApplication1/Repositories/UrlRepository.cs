using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class UrlRepository
{
    private static UrlRepository _repository;
    private static readonly List<Url> _urls;

    static UrlRepository()
    {
        _repository = new UrlRepository();
        _urls = new List<Url>();
        
        _urls.Add(new Url("https://en.wikipedia.org/wiki/ASP.NET", "17YZvvxOyV", "user@gmail", DateTime.Now));
        _urls.Add(new Url("https://www.youtube.com/watch?v=EUAIulcmNYI", "BQ8ssyEzav", "user@gmail", DateTime.Now));
        _urls.Add(new Url("https://en.wikipedia.org/wiki/Angular_(web_framework)", "mKDlkUadE7", "asd@f", DateTime.Now));
        _urls.Add(new Url("https://djinni.co/my/dashboard/", "yhsVB4poQX", "asd@f", DateTime.Now));
    }

    public static UrlRepository GetRepository()
    {
        return _repository;
    }
    
    public List<Url> GetUrls()
    {
        return _urls;
    }

    public Url? GetUrl(int id)
    {
        return _urls.Find(item => item.Id == id);
    }
    
    public Url? GetByShortUrl(string shortUrl)
    {
        return _urls.Find(item => item.ShortUrl == shortUrl);
    }
    
    public bool HasFullUrl(string fullUrl)
    {
        return _urls.Find(item => item.FullUrl == fullUrl) != null;
    }
    
    public bool HasShortUrl(string shortUrl)
    {
        return _urls.Find(item => item.ShortUrl == shortUrl) != null;
    }

    public void AddUrl(Url url)
    {
        if (HasFullUrl(url.FullUrl))
        {
            throw new Exception("such full url already exists");
        }

        if (HasShortUrl(url.ShortUrl))
        {
            throw new Exception("such short url already exists");
        }
        _urls.Add(url);
    }

    public void DeleteUrl(int id)
    {
        if (_urls.Find(item => item.Id == id) == null)
        {
            throw new Exception("no such url");
        }
        _urls.Remove(_urls.Find(item => item.Id == id));
    }
}