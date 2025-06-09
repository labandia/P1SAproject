using System;
using System.Security.Cryptography;

namespace ProgramPartListWeb.Helper
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            // Generate a 128-bit salt (16 bytes)
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Generate the hash using PBKDF2 with HMACSHA256
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32); // 256-bit hash
                return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
            }
        }

        public static bool VerifyPassword(string hashedPassword, string inputPassword)
        {
            var parts = hashedPassword.Split(':');
            if (parts.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] storedHash = Convert.FromBase64String(parts[1]);

            using (var pbkdf2 = new Rfc2898DeriveBytes(inputPassword, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] testHash = pbkdf2.GetBytes(32);
                return AreHashesEqual(storedHash, testHash);
            }
        }


        // Manual constant-time comparison
        private static bool AreHashesEqual(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            int result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result |= a[i] ^ b[i];
            }
            return result == 0;
        }
    }
}