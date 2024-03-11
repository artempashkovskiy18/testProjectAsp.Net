namespace WebApplication1.Models;

public class Url
{
    public Url(string fullUrl, string shortUrl, string creator, DateTime creationDate)
    {
        Id = _idIterator++;
        FullUrl = fullUrl;
        ShortUrl = shortUrl;
        Creator = creator;
        CreationDate = creationDate;
    }

    public int Id { get; }
    public string FullUrl { get; set; }
    public string ShortUrl { get; set; }
    public string Creator { get; set; }
    public DateTime CreationDate { get; set; }
    private static int _idIterator;
}