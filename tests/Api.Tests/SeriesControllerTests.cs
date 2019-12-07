using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cemiyet.Api.Tests.Extensions;
using Cemiyet.Core.Entities;
using Cemiyet.Persistence.Application.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Cemiyet.Api.Tests
{
    public class SeriesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public SeriesControllerTests(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _httpClient = webApplicationFactory.WithWebHostBuilder(builder =>
            {
                builder.UseTestServer();
                builder.UseEnvironment("Test");
            }).CreateClient();
        }

        [Fact]
        public async Task Add_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.PostAsJsonAsync("series/", default(Serie));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            response = await _httpClient.PostAsJsonAsync("series/", new Serie
            {
                Title = ""
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Add_WithCorrectData_ShouldReturn_OK()
        {
            var response = await _httpClient.PostAsJsonAsync("series/", new Serie
            {
                Title = "Yayıncılık Serisi",
                Description = ""
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task AddBook_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var series = await _httpClient.AssertedGetEntityListFromUri<SerieViewModel>("series");
            var response = await _httpClient.PostAsJsonAsync($"series/{series.First().Id}/books", new { });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            response = await _httpClient.PostAsJsonAsync($"series/{series.First().Id}/books", new
            {
                Books = new Dictionary<Guid, short>
                {
                    {Guid.Empty, 5},
                    {Guid.NewGuid(), 0}
                }
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task AddBook_WithCorrectData_ShouldReturn_OK()
        {
            var series = await _httpClient.AssertedGetEntityListFromUri<SerieViewModel>("series");
            var books = await _httpClient.AssertedGetEntityListFromUri<BookViewModel>("books");

            var response = await _httpClient.PostAsJsonAsync($"series/{series.First().Id}/books", new
            {
                Books = new Dictionary<Guid, short>
                {
                    {books.Last().Id, 500},
                    {books.Skip(Math.Max(0, books.Count() - 2)).First().Id, short.MaxValue}
                }
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task List_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            await _httpClient.AssertedGetAsync("series?page=-1&pageSize=-5", HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task List_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            await _httpClient.AssertedGetEntityListFromUri<SerieViewModel>("series");
        }

        [Fact]
        public async Task Details_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            await _httpClient.AssertedGetAsync($"series/{Guid.NewGuid()}", HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Details_WithCorrectId_ShouldReturn_SerieObject()
        {
            var series = await _httpClient.AssertedGetEntityListFromUri<SerieViewModel>("series");
            var response = await _httpClient.AssertedGetAsync($"series/{series.First().Id}", HttpStatusCode.OK);
            var responseData = await response.Content.ReadAsAsync<SerieViewModel>();
            Assert.NotNull(responseData);
        }

        [Fact]
        public async Task DeleteOne_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.DeleteAsync($"series/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOne_WithCorrectId_ShouldReturn_OK()
        {
            var series = await _httpClient.AssertedGetEntityListFromUri<SerieViewModel>("series");
            var response = await _httpClient.DeleteAsync($"series/{series.Last().Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOneBook_WithoutCorrectIds_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.DeleteAsync($"series/{Guid.NewGuid()}/books/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOneBook_WithCorrectIds_ShouldReturn_OK()
        {
            var series = await _httpClient.AssertedGetEntityListFromUri<SerieViewModel>("series");
            var serie = series.First();
            var bookId = serie.Books.Select(sb => sb.Book).First().Id;
            var response = await _httpClient.DeleteAsync($"series/{serie.Id}/books/{bookId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
