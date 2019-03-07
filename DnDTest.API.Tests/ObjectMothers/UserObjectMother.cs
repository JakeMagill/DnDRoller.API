using DnDRoller.API.Application.DTOs;
using DnDRoller.API.Domain.Entities;
using DnDRoller.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDRoller.API.Tests.ObjectMothers
{
    public class  UserObjectMother : UserDTO
    {
        public UserObjectMother()
        {

        }

        public UserObjectMother(string firstName, string lastName, string email, string username, string password)
        {
            this.Firstname = firstName;
            this.Id = Guid.NewGuid();
            this.Lastname = lastName;
            this.Password = password;
            this.Email = email;
            this.Username = username;
        }

        public static UserObjectMother CreateBasicUser()
        {
            UserObjectMother returnUser = new UserObjectMother
            {
                Firstname = "Tester",
                Lastname = "Tester",
                Email = "Test@Test.com",
                Username = "TestUsername",
                Password = "TestPassword"
            };

            return returnUser;
        }
    }
}
