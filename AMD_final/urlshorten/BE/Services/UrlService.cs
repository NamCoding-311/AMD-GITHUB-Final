namespace URLShortener.API.Services
{
    public class UrlService
    {
        public string GenerateCode()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var rand = new Random();
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[rand.Next(s.Length)]).ToArray());
        }
    }
}
