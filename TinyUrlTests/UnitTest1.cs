using System;
using Xunit;

public class TinyUrlTests
{
    [Fact]
    public void Create_And_Retrieve_Url()
    {
        var service = new TinyUrlService();
        var longUrl = "https://example.com";
        var shortUrl = service.CreateShortUrl(longUrl);
        
        Assert.NotNull(shortUrl);
        Assert.Equal(longUrl, service.GetLongUrl(shortUrl));
    }

    [Fact]
    public void Track_Clicks()
    {
        var service = new TinyUrlService();
        var longUrl = "https://example.com";
        var shortUrl = service.CreateShortUrl(longUrl);

        service.GetLongUrl(shortUrl);
        service.GetLongUrl(shortUrl);
        
        Assert.Equal(2, service.GetClickCount(shortUrl));
    }

    [Fact]
    public void Delete_Url()
    {
        var service = new TinyUrlService();
        var longUrl = "https://example.com";
        var shortUrl = service.CreateShortUrl(longUrl);

        Assert.True(service.DeleteShortUrl(shortUrl));
        Assert.Null(service.GetLongUrl(shortUrl));
    }
}
