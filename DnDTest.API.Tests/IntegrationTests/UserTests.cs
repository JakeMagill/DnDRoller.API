using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DnDRoller.API.Domain.Services;
using DnDRoller.API.Tests.ObjectMothers;
using DnDRoller.API.Infrastructure.Contexts;
using DnDRoller.API.Infrastructure.Repositories;

namespace DnDRoller.API.Tests.IntegrationTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public async void Create_New_User_Using_Regular_User()
        {

            //Arrange - Set up the in memory context options and test user profile
            var userProfile = UserObjectMother.CreateBasicUser();
            string userPassword = "TestPassword";

            var options = new DbContextOptionsBuilder<UserContext>()
                                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                      .Options;

            //Act - Insert all of the seed data
            using (UserContext context = new UserContext(options))
            {
                UserService service = new UserService(new UserRepository(context));
                await service.Create(userProfile, userPassword);
            }

            //Assert - Clean instance of the context to run the tests
            using (UserContext context = new UserContext(options))
            {
                var returnUser = context.Users
                                    .Where(x => x.Id == userProfile.Id)
                                    .FirstOrDefault();

                Assert.IsNotNull(returnUser);
                Assert.AreEqual(userProfile.Firstname, returnUser.Firstname);
                Assert.AreEqual(userProfile.Email, returnUser.Email);
            }
        }
    }
}
