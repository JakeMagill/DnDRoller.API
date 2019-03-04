using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DnDRoller.API.Application.DTOs;
using DnDRoller.API.Domain.Entities;

namespace DnDRoller.API.Application.Interfaces
{
    public interface IUserService
    {
         Task<User> Create(UserDTO user);
         Task<UserDTO> Authenticate(string username, string password);
    }
}
