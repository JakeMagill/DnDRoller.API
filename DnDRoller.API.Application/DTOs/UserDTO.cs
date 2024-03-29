﻿using Microsoft.IdentityModel.Tokens;
using System;

namespace DnDRoller.API.Application.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string token { get; set; }
    }
}
