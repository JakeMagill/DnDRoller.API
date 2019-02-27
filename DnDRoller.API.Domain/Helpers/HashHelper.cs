using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DnDRoller.API.Domain.Helpers
{
    public static class HashHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if(password == null)
            {
                throw new ArgumentException("Password is null");
            }

            if(string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace");
            }

            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, byte [] storedHash, byte [] storedSalt)
        {
            if (password == null)
            {
                throw new ArgumentNullException("Password cannot be null");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            }

            if (storedHash.Length != 64)
            {
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            }

            if (storedSalt.Length != 128)
            {
                if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");
            }

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int x = 0; x < computedHash.Length; x++)
                {
                    if (computedHash[x] != storedHash[x])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
