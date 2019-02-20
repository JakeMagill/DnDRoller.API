using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DnDRoller.API.Domain.DTOs;
using DnDRoller.API.Domain.Entities;

namespace DnDRoller.API.Domain.Services
{
    public interface IUserService
    {
         Task<User> Create(User user, string password);
         Task<UserDTO> Authenticate(string username, string password);
         Task<bool> VerifyPassword(string password, out byte[] storedHash, out byte[] storedSalt);
    }
}
