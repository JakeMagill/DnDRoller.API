using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DnDRoller.API.Domain.Helpers;
using DnDRoller.API.Domain.DTOs;

namespace DnDRoller.API.Domain.Services
{
    public class UserService : IUserService
    {
        public Task<UserDTO> Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> Create(UserDTO user)
        {
            //Async over sync?
            user.Password = await Task.Run(() => HashHelper.HashPassword(user.Password));

            return user;
        }
    }
}
