using ASP_CORE_REACT;
using ASP_CORE_REACT.Models;
using ASP_CORE_REACT.Controllers;
using Autofac.Extras.Moq;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.EntityFrameworkCore;
using AutoFixture.Xunit2;
using ASP_CORE_REACT.classes;

namespace XUnitTests
{
        public class UserServiceTest //: IClassFixture<WebApplicationFactory<Startup>>
        {
        private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }

        [Fact]
        public void GetUsers_ValidCall()
        {
            //Arrange
            var users = GetSampleUsers();
            var usersMock = CreateDbSetMock(users);


            Mock<BloggingDBContext> mock = new Mock<BloggingDBContext>();
                mock.Setup(x => x.Users)
                    .Returns(usersMock.Object);

            //Act
                var ctrl = new UsersService(mock.Object);
                var expected = GetSampleUsers();
                var actual = ctrl.GetUsers();

            //Assert
                Assert.True(actual != null);
                Assert.Equal(expected.Count, actual.Count());
        }

        [Fact]
        public void GetUserNameById_ShouldValidPropId()
        {
            var users = GetSampleUsers();
            var usersMock = CreateDbSetMock(users);

            Mock<BloggingDBContext> mock = new Mock<BloggingDBContext>();
            mock.Setup(x => x.Users)
                .Returns(usersMock.Object);

            //Act
            var ctrl = new UsersService(mock.Object);
            var expected = new UserData { UserName = "admin", UserSurname = "admin" };
            var actual = ctrl.GetUserNameById(1);

            Assert.True(actual != null);
            Assert.Equal(expected.UserName, actual.UserName);
            Assert.Equal(expected.UserSurname, actual.UserSurname);
        }
        public List<Users> GetSampleUsers()
        {
            var result = new List<Users>()
            {
                new Users
                {
                    UserId = 1,
                    UserName = "admin",
                    UserSurname = "admin",
                    Email = "admin@gmail.com",
                    PasswordHash = "admin"
                },
                new Users
                {
                    UserId = 2,
                    UserName = "zdzich",
                    UserSurname = "nowak",
                    Email = "zdzichu@gmail.com",
                    PasswordHash = "zdzichu"
                },
                new Users
                {
                    UserId = 3,
                    UserName = "marian",
                    UserSurname = "kowalski",
                    Email = "kowal3@gmail.com",
                    PasswordHash = "kowal123"
                }
            };

            return result;
        }

        [Theory, AutoData]
        public void AddUser_Shoulde(string expectedEmail, string expectedName, string expectedSurname)
        {
            // Arrange
            var userContextMock = new Mock<BloggingDBContext>();
            userContextMock.Setup(x => x.Users.Add(It.IsAny<Users>()));

            var ctrl = new UsersService(userContextMock.Object);
            // Act
            var user = ctrl.AddUser( new Users {
                UserName = expectedName,
                Email = expectedEmail,
                UserSurname = expectedSurname
            });

            // Assert
            Assert.Equal(expectedEmail, user.Email);
            Assert.Equal(expectedName, user.UserName);
            Assert.Equal(expectedSurname, user.UserSurname);

            userContextMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        //    private readonly WebApplicationFactory<Startup> _factory;
        //    public UserControllerTest(WebApplicationFactory<Startup> factory)
        //    {
        //        _factory = factory;
        //    }
        //
        //
        //[Theory]
        //[InlineData("/api/Users/GetUserNameById/20")]
        //public async Task GetUserNameById_ShouldReturnJson(string url)
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();
        //    // Act
        //    var response = await client.GetAsync(url);
        //    // Assert
        //    response.EnsureSuccessStatusCode(); // Status Code 200-299
        //    Assert.Equal("application/json; charset=utf-8",
        //        response.Content.Headers.ContentType.ToString());
        //}
    }
}
