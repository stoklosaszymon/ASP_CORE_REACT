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

namespace XUnitTests
{
        public class UserControllerTest : IClassFixture<WebApplicationFactory<Startup>>
        {
        [Fact]
        public void GetUsers_ValidCall()
        {
            //Arrange
            var usersMock = new Mock<DbSet<Users>>();
            usersMock.As<IQueryable<Users>>().Setup(m => m.Provider).Returns(GetSampleUsers().AsQueryable().Provider);
            usersMock.As<IQueryable<Users>>().Setup(m => m.Expression).Returns(GetSampleUsers().AsQueryable().Expression);
            usersMock.As<IQueryable<Users>>().Setup(m => m.ElementType).Returns(GetSampleUsers().AsQueryable().ElementType);
            usersMock.As<IQueryable<Users>>().Setup(m => m.GetEnumerator()).Returns(GetSampleUsers().AsQueryable().GetEnumerator);


            Mock<BloggingDBContext> mock = new Mock<BloggingDBContext>();
                mock.Setup(x => x.Users)
                    .Returns(usersMock.Object);

            //Act
                var ctrl = new UsersController(mock.Object);
                var expected = GetSampleUsers();
                var actual = ctrl.GetUsers();

            //Assert
                Assert.True(actual != null);
                Assert.Equal(expected.Count, actual.Count());
        }

        public List<Users> GetSampleUsers()
        {
            var result = new List<Users>()
            {
                new Users
                {
                    Email = "admin@gmail.com",
                    PasswordHash = "admin"
                },
                new Users
                {
                    Email = "zdzichu@gmail.com",
                    PasswordHash = "zdzichu"
                }
            };

            return result;
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
