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

        public async Task<User> Create(UserDTO user)
        {
            User createdUser = new User()
            {
                Firstname = user.Firstname, 
                Lastname = user.Lastname,
                Email = user.Email,
                Username = user.Username,
                Id = Guid.NewGuid()
            };

            byte[] passwordHash, passwordSalt;
            HashHelper.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            createdUser.PasswordHash = passwordHash;
            createdUser.PasswordSalt = passwordSalt;

            return createdUser;
        }

        public Task<bool> VerifyPassword(string password, out byte[] storedHash, out byte[] storedSalt)
        {
            throw new NotImplementedException();
        }
    }
}
