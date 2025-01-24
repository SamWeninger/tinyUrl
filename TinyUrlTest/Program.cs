public class TinyUrlService
{
    private readonly Dictionary<string, (string LongUrl, int Clicks)> urlMap = []; // data structure for storing urls
    private readonly Random rand = new(); // rand to generate urls
    private const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // characters for url
    private const int urlLength = 16; // max length random short url
    private const string tinyUrlPrefix = "tinyurl.com/";

    // 1. make short url
    public string CreateShortUrl(string longUrl, string? customShortUrl = null)
    {
        string shortUrl = customShortUrl ?? GenerateShortUrl();
        
        if (urlMap.ContainsKey(shortUrl))
            throw new Exception("Short URL exists.");
        
        urlMap[shortUrl] = (longUrl, 0);
        return shortUrl;
    }

    // get long url and increment click count
    public string? GetLongUrl(string shortUrl)
    {
        if (urlMap.TryGetValue(shortUrl, out (string longUrl, int clicks) value))
        {
            var (longUrl, clicks) = value;
            urlMap[shortUrl] = (longUrl, clicks + 1);
            return longUrl;
        }
        return null;
    }

    public bool DeleteShortUrl(string shortUrl)
    {
        return urlMap.Remove(shortUrl);
    }

    public int GetClickCount(string shortUrl)
    {
        return urlMap.TryGetValue(shortUrl, out var entry) ? entry.Clicks : 0;
    }

    // create url if short url is not provided
    private string GenerateShortUrl()
    {
        // continue generating urls until unique url is created
        while (true)
        {
            char[] shortUrlChars = new char[urlLength];
            
            for (int i = 0; i < urlLength; i++)
            {
                shortUrlChars[i] = chars[rand.Next(chars.Length)];
            }

            string shortUrl = new string(shortUrlChars);

            if (!urlMap.ContainsKey(shortUrl))
                return shortUrl;
        }
    }
}

class Program
{
    static void Main()
    {
    }
}