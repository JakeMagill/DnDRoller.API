using DnDRoller.API.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DnDRoller.API.Domain.Services
{
    public interface IUserService
    {
         Task<UserModel> Create(UserModel user);
         Task<UserModel> Authenticate(string username, string password);
    }
}
