using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace URLShortener.API.Services
{
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using System.Security.Cryptography;

    public class AuthService
    {
        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32
            ));

            return $"{Convert.ToBase64String(salt)}.{hash}";
        }

        public bool VerifyPassword(string password, string savedHash)
        {
            var parts = savedHash.Split('.');
            var salt = Convert.FromBase64String(parts[0]);

            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32
            ));

            return hash == parts[1];
        }
    }
}
