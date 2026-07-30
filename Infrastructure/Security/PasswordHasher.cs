using Application.Services;
using System.Security.Cryptography;


namespace Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int saltSize = 16;
        private const int keySize = 32;
        private const int iterations = 100000;

        public string Hash(string password)
        {
            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[saltSize];
            rng.GetBytes(salt);
            
            using var pbkdf2 = new Rfc2898DeriveBytes(password,salt, iterations,HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(keySize);

            var result = new byte[saltSize+keySize];
            Buffer.BlockCopy(salt, 0, result, 0, saltSize);
            Buffer.BlockCopy(key,0,result, saltSize, keySize);

            return Convert.ToBase64String(result);
        }

        public bool Verify(string password, string hash)
        {
            var hashBytes = Convert.FromBase64String(hash);

            var salt = new byte[saltSize];
            Buffer.BlockCopy(hashBytes, 0, salt, 0, saltSize);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(keySize);

            for (int i = 0; i < keySize; i++)
            {
                if (hashBytes[saltSize + i] != key[i])
                    return false;
            }

            return true;
        }
    }
}
