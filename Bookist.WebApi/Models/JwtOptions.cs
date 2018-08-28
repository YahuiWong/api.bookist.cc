using System;

namespace Bookist.WebApi
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int Expiration { get; set; } = TimeSpan.FromMinutes(30).Seconds;
    }
}
