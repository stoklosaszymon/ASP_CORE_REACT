using ASP_CORE_REACT;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTests
{
        public class PersonTests : IClassFixture<WebApplicationFactory<Startup>>
        {
            private readonly WebApplicationFactory<Startup> _factory;
            public PersonTests(WebApplicationFactory<Startup> factory)
            {
                _factory = factory;
            }


        [Theory]
        [InlineData("/api/Users/GetUserNameById/20")]
        public async Task ApiRouteTest(string url)
        {
            // Arrange
            var client = _factory.CreateClient();
            // Act
            var response = await client.GetAsync(url);
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
