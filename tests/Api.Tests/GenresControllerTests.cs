using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cemiyet.Application.Genres.Commands.DeleteMany;
using Cemiyet.Core.Entities;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace Cemiyet.Api.Tests
{
    public class GenresControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public GenresControllerTests(WebApplicationFactory<Startup> webApplicationFactory)
        {
            using var scope = webApplicationFactory.Services.GetService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDataContext>();

            AppDataContextSeed.Seed(context);

            _httpClient = webApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task Add_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.PostAsJsonAsync("genres/", default(Genre));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Add_WithCorrectData_ShouldReturn_OK()
        {
            var response = await _httpClient.PostAsJsonAsync("genres/", new Genre
            {
                Name = "test"
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task List_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.GetAsync("genres?page=-1&pageSize=-5");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task List_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            var response = await _httpClient.GetAsync("genres");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseData = await response.Content.ReadAsAsync<List<GenreViewModel>>();
            Assert.NotEmpty(responseData);
        }

        [Fact]
        public async Task ListBooks_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            var genresResponse = await _httpClient.GetAsync("genres");
            var genres = await genresResponse.Content.ReadAsAsync<List<GenreViewModel>>();

            var response = await _httpClient.GetAsync($"genres/{genres.First().Id}/books?page=-1&pageSize=-5");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ListBooks_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            var genresResponse = await _httpClient.GetAsync("genres");
            var genres = await genresResponse.Content.ReadAsAsync<List<GenreViewModel>>();

            var response = await _httpClient.GetAsync($"genres/{genres.First().Id}/books");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Details_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.GetAsync($"genres/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Details_WithCorrectId_ShouldReturn_GenreObject()
        {
            var genresResponse = await _httpClient.GetAsync("genres");
            var genres = await genresResponse.Content.ReadAsAsync<List<GenreViewModel>>();

            var response = await _httpClient.GetAsync($"genres/{genres.First().Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseData = await response.Content.ReadAsAsync<Genre>();
            Assert.NotNull(responseData);
        }

        [Fact]
        public async Task Update_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.PutAsJsonAsync($"genres/{Guid.NewGuid()}", default(Genre));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var genresResponse = await _httpClient.GetAsync("genres");
            var genres = await genresResponse.Content.ReadAsAsync<List<Genre>>();

            var response = await _httpClient.PutAsJsonAsync($"genres/{genres.First().Id}", default(Genre));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithCorrectData_ShouldReturn_OK()
        {
            var genresResponse = await _httpClient.GetAsync("genres");
            var genres = await genresResponse.Content.ReadAsAsync<List<GenreViewModel>>();

            var response = await _httpClient.PutAsJsonAsync($"genres/{genres.Last().Id}", new
            {
                Name = "update-deneme"
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOne_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.DeleteAsync($"genres/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOne_WithCorrectId_ShouldReturn_OK()
        {
            var genresResponse = await _httpClient.GetAsync("genres");
            var genres = await genresResponse.Content.ReadAsAsync<List<Genre>>();

            var response = await _httpClient.DeleteAsync($"genres/{genres.Last().Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMany_WithoutCorrectIds_ShouldReturn_BadRequest()
        {
            string[] ids = { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress + "genres"),
                Content = new StringContent(JsonConvert.SerializeObject(ids), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMany_WithCorrectIds_ShouldReturn_OK()
        {
            var genresResponse = await _httpClient.GetAsync("genres");
            var genres = await genresResponse.Content.ReadAsAsync<List<Genre>>();

            var dmc = new DeleteManyCommand { Ids = genres.TakeLast(2).Select(g => g.Id).ToArray() };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress + "genres"),
                Content = new StringContent(JsonConvert.SerializeObject(dmc), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
