using System;
using System.Threading.Tasks;
using DnDRoller.API.Domain.Entities;

public interface IUserRepository
{
    Task<bool> CreateUser(User user);
}