using DnDRoller.API.Application.DTOs;
using DnDRoller.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDRoller.API.Tests.ObjectMothers
{
    public class  UserObjectMother : UserDTO
    {
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
