using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Library.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Library.Tests.Integration
{
    public class BookTest
    {
        private readonly HttpClient _client;

        public BookTest()
        {
            // Arrange
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Fact]
        public async Task Get_Should_Return_Success()
        {
            // Act
            var response = await _client.GetAsync("/api/book");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            
            // Assert
            responseString.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_By_Id_Should_Return_Success()
        {
            // Act
            var response = await _client.GetAsync("/api/book/1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            responseString.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_By_Id_Should_Return_NotFound()
        {
            // Act
            var response = await _client.GetAsync("/api/book/999999");

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
