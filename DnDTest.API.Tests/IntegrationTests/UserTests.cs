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
using System.Collections.Generic;
using DnDRoller.API.Domain.Entities;

namespace DnDRoller.API.Tests.IntegrationTests
{
    [TestClass]
    public class UserTests
    {
        private IMapper _mapper;
        dynamic DbContextOptions;

        [TestInitialize]
        public void Initalize()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMapper());
            });

             _mapper = mapperConfig.CreateMapper();

            DbContextOptions = new DbContextOptionsBuilder<DatabaseService>()
                                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                      .Options;

            User userOne = new User();
        }


        public List<Guid> CreateTestUsers()
        {
            List<Guid> ids = new List<Guid>();
            List<UserObjectMother> DTOs = new List<UserObjectMother>();
            List<User> users = new List<User>();
            UserObjectMother userOne = new UserObjectMother("Curtis", "Tarr", "Curtis.Tarr@Test.com", "Antifis", "Password");
            UserObjectMother userTwo = new UserObjectMother("Max", "Magill", "Max.Magill@Test.com", "ComradicalMax", "Password");
            UserObjectMother userThree = new UserObjectMother("Jake", "Magill", "Jake.Magill@Test.com", "Pazzda", "Password");

            DTOs.Add(userOne);
            DTOs.Add(userTwo);
            DTOs.Add(userThree);

            using (DatabaseService context = new DatabaseService(DbContextOptions))
            {
                for (int x = 0; x < DTOs.Count; x++)
                {
                    User mappedUser = new User();
                    mappedUser = _mapper.Map<User>(DTOs[x]);
                    mappedUser.SetDefaults();
                    context.Users.Add(mappedUser);
                    context.SaveChanges();

                    users.Add(mappedUser);
                }
            }

            foreach (var user in users)
            {
                ids.Add(user.Id);
            }

            return ids;
        }

        [TestMethod]
        public async Task CreateNewRegularUser()
        {
            //Arrange - Set up the in memory context options and test user profile
            var userProfile = UserObjectMother.CreateBasicUser();

            var mockTokenService = new Mock<ITokenService>();

            //Act - Insert all of the seed data
            using (DatabaseService context = new DatabaseService(DbContextOptions))
            {
                UserService service = new UserService(_mapper, context, mockTokenService.Object);

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

        [TestMethod]
        public async Task DeleteUser()
        {
            //Arrange
            List<Guid> ids = this.CreateTestUsers();
            var mockTokenService = new Mock<ITokenService>();

            //Act
            using (DatabaseService context = new DatabaseService(DbContextOptions))
            {
                UserService service = new UserService(_mapper, context, mockTokenService.Object);

                var user = context.Users
                    .Where(x => x.Id == ids[0]).FirstOrDefault();

                if (user == null)
                {
                    throw new Exception();
                }

                await service.Delete(user.Id);

                //Assert
                Assert.IsNull(context.Users
                    .Where(x => x.Id == ids[0]).FirstOrDefault());
            }
        }

        [TestMethod]
        public async Task UpdateUser()
        {
            
        }
    }

}
