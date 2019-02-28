using System;
using System.Text;
using System.Threading.Tasks;
using DnDRoller.API.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using DnDRoller.API.Application.Helpers;
using DnDRoller.API.Application.DTOs;
using AutoMapper;
using DnDRoller.API.Application.Interfaces;

namespace DnDRoller.API.Application.Services
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        private IDatabaseService _databaseService;

        public UserService(IMapper mapper, IDatabaseService databaseService)
        { 
            _mapper = mapper;
            _databaseService = databaseService;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            User returnUser = await _databaseService.Users.FindAsync(username);

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

        public async Task<User> Create(UserDTO dto)
        {
            byte[] passwordHash, passwordSalt;
            User user = _mapper.Map<User>(dto);

            HashHelper.CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            user.SetInitiallDefaults();

            _databaseService.Users.Add(user);
            _databaseService.Save();

            return user;
        }
    }
}
