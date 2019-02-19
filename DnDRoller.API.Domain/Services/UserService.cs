using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DnDRoller.API.Domain.Helpers;
using DnDRoller.API.Domain.ViewModels;

namespace DnDRoller.API.Domain.Services
{
    public class UserService : IUserService
    {
        public Task<UserModel> Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> Create(UserModel user)
        {
            //Async over sync?
            user.Password = await Task.Run(() => HashHelper.HashPassword(user.Password));

            return user;
        }
    }
}
