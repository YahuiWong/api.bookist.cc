namespace Bookist.WebApi
{
    public class JwtResult
    {
        public string AccessToken { get; set; }
        
        public long ExpiresIn { get; set; }
    }
}
