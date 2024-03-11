using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class UrlShortenerService
{
    private int _numberOfSymbolsInShortUrl = 10;
    private string _alhabet = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz1234567890";
    private static UrlShortenerService _service;
    private UrlRepository _repository = UrlRepository.GetRepository();

    static UrlShortenerService()
    {
        _service = new UrlShortenerService();
    }

    public static UrlShortenerService GetService()
    {
        return _service;
    }

    public string GetShortenedUrl()
    {
        char[] shortUrl = new char[_numberOfSymbolsInShortUrl];
        Random rnd = new Random();
        do
        {
            for (int i = 0; i < _numberOfSymbolsInShortUrl; i++)
            {
                shortUrl[i] = _alhabet[rnd.Next(_alhabet.Length - 1)];
            }
        } while (_repository.HasShortUrl(shortUrl.ToString()));

        return new string(shortUrl);
    }
}