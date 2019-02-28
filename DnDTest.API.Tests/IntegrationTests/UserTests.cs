using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DnDRoller.API.Tests.ObjectMothers;
using DnDRoller.API.Infrastructure.Contexts;
using DnDRoller.API.Application.Services;
using DnDRoller.API.Application.Interfaces;
using DnDRoller.API.Application.Mappers;
using System.Threading.Tasks;
using Moq;
using AutoMapper;

namespace DnDRoller.API.Tests.IntegrationTests
{
    [TestClass]
    public class UserTests
    {
        private Mapper mapper;
        dynamic options;

        [TestInitialize]
        public void Initalize()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMapper());
            });

            mapper = (Mapper)mockMapper.CreateMapper();

            options = new DbContextOptionsBuilder<DatabaseService>()
                                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                      .Options;
        }

        [TestMethod]
        public async Task CreateNewRegularUser()
        {
            //Arrange - Set up the in memory context options and test user profile
            var userProfile = UserObjectMother.CreateBasicUser();

            //Act - Insert all of the seed data
            using (DatabaseService context = new DatabaseService(options))
            {
                UserService service = new UserService(mapper, context);

                var returnUser = await service.Create(userProfile);

                var dbUser = context.Users
                                    .Where(x => x.Id == returnUser.Id)
                                    .FirstOrDefault();

                //Assert
                Assert.IsNotNull(dbUser);
                Assert.AreEqual(userProfile.Firstname, dbUser.Firstname);
                Assert.AreEqual(userProfile.Email, dbUser.Email);
            }

        }
    }

}
