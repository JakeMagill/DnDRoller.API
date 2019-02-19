using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DnDRoller.API.Domain.Helpers
{
    public static class HashHelper
    {
        public static string HashPassword(string password)
        {
            //1. Create a salt value with CryptographicRNG
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            //2. Create Rfc2898DeriveBytes and get hash value
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 5000);
            byte[] hash = pbkdf2.GetBytes(20);

            //3. Combine the salt and password bytes for later use
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            //4. Turn the combined salt+hash into a string for storage
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
        }

        public static bool VerifyPassword(string hashedPassword, string unHashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(unHashedPassword, salt, 5000);
            byte[] hash = pbkdf2.GetBytes(20);

            for(int x = 0; x < 20; x++)
            {
                if(hashBytes[x+16] != hash[x])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
