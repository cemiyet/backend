using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cemiyet.Application.Authors.Commands.DeleteMany;
using Cemiyet.Core.Entities;
using Newtonsoft.Json;
using Xunit;

namespace Cemiyet.Api.Tests
{
    public class AuthorsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Add_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.PostAsJsonAsync("authors/", default(Author));
            var response2 = await _httpClient.PostAsJsonAsync("authors/", new Author
            {
                Name = "",
                Surname = "V3L1"
            });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);
        }

        [Fact]
        public async Task Add_WithCorrectData_ShouldReturn_OK()
        {
            var response = await _httpClient.PostAsJsonAsync("authors/", new Author
            {
                Name = "Yazar",
                Surname = "Veli"
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

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

        [Fact]
        public async Task Details_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.GetAsync($"authors/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Details_WithCorrectId_ShouldReturn_AuthorObject()
        {
            var authorsResponse = await _httpClient.GetAsync("authors");
            var authors = await authorsResponse.Content.ReadAsAsync<List<Author>>();

            var response = await _httpClient.GetAsync($"authors/{authors.First().Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseData = await response.Content.ReadAsAsync<Author>();
            Assert.NotNull(responseData);
        }

        [Fact]
        public async Task UpdatePartially_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var authorsResponse = await _httpClient.GetAsync("authors");
            var authors = await authorsResponse.Content.ReadAsAsync<List<Author>>();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri(_httpClient.BaseAddress + $"authors/{authors.First().Id}"),
                Content = new StringContent(JsonConvert.SerializeObject(new { }), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePartially_WithCorrectData_ShouldReturn_OK()
        {
            var authorsResponse = await _httpClient.GetAsync("authors");
            var authors = await authorsResponse.Content.ReadAsAsync<List<Author>>();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri(_httpClient.BaseAddress + $"authors/{authors.First().Id}"),
                Content = new StringContent(JsonConvert.SerializeObject(new { Name = "YAZAR" }), Encoding.UTF8,
                                            "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.PutAsJsonAsync($"authors/{Guid.NewGuid()}", default(Author));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var authorsResponse = await _httpClient.GetAsync("authors");
            var authors = await authorsResponse.Content.ReadAsAsync<List<Author>>();

            var response = await _httpClient.PutAsJsonAsync($"authors/{authors.First().Id}", default(Author));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            response = await _httpClient.PutAsJsonAsync($"authors/{authors.First().Id}", new Author
            {
                Name = "5",
                Surname = null,
                Bio = default
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithCorrectData_ShouldReturn_OK()
        {
            var authorsResponse = await _httpClient.GetAsync("authors");
            var authors = await authorsResponse.Content.ReadAsAsync<List<Author>>();

            var response = await _httpClient.PutAsJsonAsync($"authors/{authors.Last().Id}", new
            {
                Name = "Name",
                Surname = "Surname",
                Bio = "Bio"
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOne_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.DeleteAsync($"authors/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOne_WithCorrectId_ShouldReturn_OK()
        {
            var authorsResponse = await _httpClient.GetAsync("authors");
            var authors = await authorsResponse.Content.ReadAsAsync<List<Author>>();

            var response = await _httpClient.DeleteAsync($"authors/{authors.Last().Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMany_WithoutCorrectIds_ShouldReturn_BadRequest()
        {
            string[] ids = { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress + "authors"),
                Content = new StringContent(JsonConvert.SerializeObject(ids), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMany_WithCorrectIds_ShouldReturn_OK()
        {
            var authorsResponse = await _httpClient.GetAsync("authors");
            var authors = await authorsResponse.Content.ReadAsAsync<List<Author>>();

            var dmc = new DeleteManyCommand { Ids = authors.TakeLast(2).Select(g => g.Id).ToArray() };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress + "authors"),
                Content = new StringContent(JsonConvert.SerializeObject(dmc), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
