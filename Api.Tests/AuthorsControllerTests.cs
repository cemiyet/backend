using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cemiyet.Core.Entities;
using Xunit;

namespace Cemiyet.Api.Tests
{
    public class AuthorsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task List_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.GetAsync("authors?page=-1&pageSize=-5");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task List_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            var response = await _httpClient.GetAsync("authors");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseData = await response.Content.ReadAsAsync<List<Author>>();
            Assert.NotEmpty(responseData);
        }
    }
}
