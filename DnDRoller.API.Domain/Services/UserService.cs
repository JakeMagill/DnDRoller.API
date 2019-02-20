using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DnDRoller.API.Domain.Helpers;
using DnDRoller.API.Domain.DTOs;
using DnDRoller.API.Domain.Entities;

namespace DnDRoller.API.Domain.Services
{
    public class UserService : IUserService
    {
        public Task<UserDTO> Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> Create(User user, string password)
        {
            user.Id = Guid.NewGuid();
            byte[] passwordHash, passwordSalt;
            HashHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return Task.FromResult(user);
        }

        public Task<bool> VerifyPassword(string password, out byte[] storedHash, out byte[] storedSalt)
        {
            throw new NotImplementedException();
        }
    }
}
