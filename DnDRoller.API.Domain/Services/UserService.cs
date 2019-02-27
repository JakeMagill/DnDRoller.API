using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DnDRoller.API.Domain.Helpers;
using DnDRoller.API.Domain.DTOs;
using DnDRoller.API.Domain.Entities;
using DnDRoller.API.Domain.Repositories;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace DnDRoller.API.Domain.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            User returnUser = await _repository.GetUserByUsernameForLogin(username);

            if (returnUser == null)
            {
                throw new Exception();
            }

            if (!HashHelper.VerifyPasswordHash(password, returnUser.PasswordHash, returnUser.PasswordSalt))
            {
                throw new Exception();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("This is a secret and all that stuff");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, returnUser.Id.ToString())
                }),
                Expires = DateTime.Now.AddDays(1), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            


            return returnUser;
        }

        public async Task<User> Create(User user, string password)
        {
            user.Id = Guid.NewGuid();
            byte[] passwordHash, passwordSalt;
            HashHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            if (await _repository.CreateUser(user))
            {
                return user;
            }

            return null;
        }

        public Task<bool> VerifyPassword(string password, out byte[] storedHash, out byte[] storedSalt)
        {
            throw new NotImplementedException();
        }
    }
}
