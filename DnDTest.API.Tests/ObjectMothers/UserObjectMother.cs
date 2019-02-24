using DnDRoller.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDRoller.API.Tests.ObjectMothers
{
    public class  UserObjectMother : User
    {
        public static UserObjectMother CreateBasicUser()
        {
            UserObjectMother returnUser = new UserObjectMother
            {
                Firstname = "Tester",
                Lastname = "Tester",
                Email = "Test@Test.com",
                Id = Guid.NewGuid(),
                Username = "TestUsername"
            };

            return returnUser;
        }
    }
}
