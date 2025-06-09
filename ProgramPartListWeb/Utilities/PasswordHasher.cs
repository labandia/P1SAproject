using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;



namespace ProgramPartListWeb.Helper
{
    public class PasswordHasher
    {
        public static string Hashpassword(string pass)
        {
            // Generate a 128-bit salt using a cryptographically strong random number generator
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Hash the password with the salt using PBKDF2 and HMACSHA256
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pass,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));

            // Return the salt and the hash separated by a colon
            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }


        public static bool VerifyPassword(string hash, string password)
        {
            // Split the stored hash into salt and hash components
            var parts = hash.Split(':');
            byte[] salt = Convert.FromBase64String(parts[0]);
            string storedHash = parts[1];

            // Hash the password with the stored salt
            string testHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));

            
            // Compare the stored hash with the newly computed hash
            return storedHash == testHash;
        }
    }
}