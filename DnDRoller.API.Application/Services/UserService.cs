using System;
using System.Threading.Tasks;
using DnDRoller.API.Domain.Entities;
using DnDRoller.API.Application.Helpers;
using DnDRoller.API.Application.DTOs;
using AutoMapper;
using DnDRoller.API.Application.Interfaces;
using System.Linq;

namespace DnDRoller.API.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseService _databaseService;
        private readonly ITokenService _tokenService;

        public UserService(IMapper mapper, IDatabaseService databaseService, ITokenService tokenService)
        {
            _mapper = mapper;
            _databaseService = databaseService;
            _tokenService = tokenService;
        }

        public async Task<UserDTO> Authenticate(string username, string password)
        {
            User user =  _databaseService.Users.Where(u => u.Username.Equals(username)).Single();

            if (user == null)
            {
                throw new Exception();
            }

            if (!HashHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception();
            }

            UserDTO returnUser = _mapper.Map<UserDTO>(user);

            returnUser.token = await _tokenService.CreateJWTToken(returnUser.Username);

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

            await _databaseService.Users.AddAsync(user);
            _databaseService.Save();

            return user;
        }
    }
}
