using DnDRoller.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DnDRoller.API.Infrastructure.Contexts
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            :base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }

}
