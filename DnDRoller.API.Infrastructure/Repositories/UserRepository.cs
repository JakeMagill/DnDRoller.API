using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DnDRoller.API.Domain.Entities;
using DnDRoller.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using DnDRoller.API.Infrastructure.Contexts;
using System.Linq;

namespace DnDRoller.API.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }
        
        public async Task<bool> CreateUser(User user)
        {
            try
            {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<User> GetUserByUsernameForLogin(string username)
        {
            var returnUser = _context.Users.Where(x => x.Username.Equals(username)).Single();
            
            if(returnUser == null)
            {
                throw new Exception();
            }

            return returnUser as User;
        }
    }
}
