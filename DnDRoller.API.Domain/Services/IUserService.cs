using DnDRoller.API.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DnDRoller.API.Domain.Services
{
    public interface IUserService
    {
         Task<UserDTO> Create(UserDTO user);
         Task<UserDTO> Authenticate(string username, string password);
    }
}
