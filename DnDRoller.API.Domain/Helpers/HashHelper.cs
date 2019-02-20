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
    }
}
