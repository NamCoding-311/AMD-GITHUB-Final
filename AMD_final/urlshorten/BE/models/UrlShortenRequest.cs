namespace URLShortener.API.Models
{
    public class UrlShortenRequest
    {
        // URL gốc mà người dùng nhập
        public string OriginalUrl { get; set; } = string.Empty;
    }
}
