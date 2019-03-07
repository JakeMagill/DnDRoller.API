using System;
using System.Security.Cryptography;
using System.Text;

namespace DnDRoller.API.Application.Helpers
{
    public static class HashHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            try
            {
                if (password == null)
                {
                    throw new ArgumentNullException("Password is null");
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentNullException("Value cannot be empty or whitespace");
                }

                using (var hmac = new HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException("Null arguement found", e);
            }
        }

        public static bool VerifyPasswordHash(string password, byte [] storedHash, byte [] storedSalt)
        {
            try
            {
                if (password == null)
                {
                    throw new ArgumentNullException("Password cannot be null");
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentNullException();
                }

                if (storedHash.Length != 64)
                {
                    throw new ArgumentException();
                }

                if (storedSalt.Length != 128)
                {
                    if (storedSalt.Length != 128)
                    {
                        throw new ArgumentException();
                    }
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
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException("Null arguement found", e);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Invalid arguement generated", e);
            }
        }
    }
}
