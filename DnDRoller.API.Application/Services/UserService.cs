using System;
using System.Threading.Tasks;
using DnDRoller.API.Domain.Entities;
using DnDRoller.API.Application.Helpers;
using DnDRoller.API.Application.DTOs;
using AutoMapper;
using DnDRoller.API.Application.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                User user = _databaseService.Users.Where(u => u.Username.Equals(username)).Single();

                if (user == null)
                {
                    throw new Exception("User does not exist");
                }

                if (!HashHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    throw new Exception("Password is incorrect");
                }

                UserDTO returnUser = _mapper.Map<UserDTO>(user);

                returnUser.token = await _tokenService.CreateJWTToken(returnUser.Username);

                return returnUser;
            }
            catch (Exception e)
            {
                throw new Exception("User could not be authenticated: " + e);

            }
        }

        public async Task<UserDTO> Create(UserDTO dto)
        {
            try
            {
                byte[] passwordHash, passwordSalt;
                User user = _mapper.Map<User>(dto);

                HashHelper.CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                user.SetDefaults();

                await _databaseService.Users.AddAsync(user);
                _databaseService.Save();

                dto = _mapper.Map<UserDTO>(user);

                return dto;
            }
            catch
            {
                throw new Exception("User could not be created due to an error");
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var user = await _databaseService.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

                if (user == null)
                {
                    return false;
                }

                _databaseService.Users.Remove(user);
                _databaseService.Save();

                return true;
            }
            catch
            {
                throw new Exception("User could not be deleted");
            }
        }

        public async Task<UserDTO> Update(UserDTO dto)
        {
            try
            {
                User userFromDb = await _databaseService.Users.Where(x => x.Id == dto.Id).SingleAsync();

                userFromDb.Firstname = dto.Firstname;
                userFromDb.Lastname = dto.Lastname;
                userFromDb.Email = dto.Email;

                await Task.Run(() => _databaseService.Users.Update(userFromDb));
                _databaseService.Save();

                return dto;
            }
            catch (Exception e)
            {
                throw new Exception("User could not be updated due to an error: " + e);
            }
        }
    }
}
