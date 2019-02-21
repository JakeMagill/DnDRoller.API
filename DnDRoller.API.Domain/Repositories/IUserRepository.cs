using System;
using System.Threading.Tasks;
using DnDRoller.API.Domain.Entities;

namespace DnDRoller.API.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> CreateUser(User user);
    }
}
