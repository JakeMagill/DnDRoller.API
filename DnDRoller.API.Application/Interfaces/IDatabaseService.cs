using System;
using System.Collections.Generic;
using System.Text;
using DnDRoller.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DnDRoller.API.Application.Interfaces
{
    public interface IDatabaseService
    {
        DbSet<User> Users { get; set; }

        void Save();
    }
}
