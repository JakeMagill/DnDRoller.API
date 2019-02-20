using System;
using System.Collections.Generic;
using System.Text;

namespace DnDRoller.API.Domain.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
