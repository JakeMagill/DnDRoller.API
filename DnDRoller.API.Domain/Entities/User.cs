using System;
using System.Threading.Tasks;
using DnDRoller.API.Domain.Common;

namespace DnDRoller.API.Domain.Entities
{
    public class User : EntityBase
    { 
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}