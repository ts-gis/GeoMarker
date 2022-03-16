using System.Text;

namespace GeoMarker.Infrastucture.Configuration
{
    public class JwtConfig
    {
        public const string Forever = "Forever";

        public const string SectionName = "Jwt";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Key { get; set; }

        public int Expire { get; set; }

        public byte[] KeyBytes =>
            string.IsNullOrEmpty(Key) ?
            throw new ArgumentNullException(Key) :
            Encoding.UTF8.GetBytes(Key);
    }
}
