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
        Task<UserDTO> Create(UserDTO user);
        Task<UserDTO> Authenticate(string username, string password);
        Task<bool> Delete(Guid id);
        Task<UserDTO> Update(UserDTO user);
        Task<UserDTO> Details(Guid id);
    }
}
