using DnDRoller.API.Application.Interfaces;
using DnDRoller.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DnDRoller.API.Infrastructure.Contexts
{
    public class DatabaseService : DbContext, IDatabaseService
    {
        public DatabaseService(DbContextOptions<DatabaseService> options)
            :base(options)
        { }

        public DbSet<User> Users { get; set; }

        public void Save()
        {
            this.SaveChanges();
        }
            
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseService).Assembly);
        }
    }

}
