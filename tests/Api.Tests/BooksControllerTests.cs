using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cemiyet.Application.Books.Commands.Add;
using Cemiyet.Application.Books.Commands.AddEdition;
using Cemiyet.Application.Books.Commands.DeleteMany;
using Cemiyet.Application.Books.Commands.DeleteManyEdition;
using Cemiyet.Core.Entities;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace Cemiyet.Api.Tests
{
    public class BooksControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public BooksControllerTests(WebApplicationFactory<Startup> webApplicationFactory)
        {
            using var scope = webApplicationFactory.Services.GetService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDataContext>();

            AppDataContextSeed.Seed(context);

            _httpClient = webApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task Add_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.PostAsJsonAsync("books/", default(Book));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var response2 = await _httpClient.PostAsJsonAsync("books/", new Book
            {
                Title = "abc",
                Description = "ddd"
            });
            Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);
        }

        [Fact]
        public async Task Add_WithCorrectData_ShouldReturn_OK()
        {
            var genreResponse = await _httpClient.GetAsync("genres?page=1&pageSize=2");
            var genreData = await genreResponse.Content.ReadAsAsync<List<GenreViewModel>>();
            Assert.NotNull(genreData);

            var authorResponse = await _httpClient.GetAsync("authors?page=1&pageSize=2");
            var authorData = await authorResponse.Content.ReadAsAsync<List<AuthorViewModel>>();
            Assert.NotNull(authorData);

            var response = await _httpClient.PostAsJsonAsync("books/", new AddCommand
            {
                Title = "abc",
                Description = "abcdesc",
                GenreIds = genreData.Select(g => g.Id).ToList(),
                AuthorIds = authorData.Select(a => a.Id).ToList()
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task AddEdition_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var booksResponse = await _httpClient.GetAsync("books");
            Assert.Equal(HttpStatusCode.OK, booksResponse.StatusCode);

            var books = await booksResponse.Content.ReadAsAsync<List<BookViewModel>>();
            Assert.NotNull(books);

            var response = await _httpClient.PostAsJsonAsync($"books/{books.First().Id}/editions", default(BookEdition));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var response2 = await _httpClient.PostAsJsonAsync($"books/{books.First().Id}/editions", new AddEditionCommand
            {
                Isbn = "",
                PageCount = short.MinValue
            });
            Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);
        }

        [Fact]
        public async Task AddEdition_WithCorrectData_ShouldReturn_OK()
        {
            var booksResponse = await _httpClient.GetAsync("books");
            Assert.Equal(HttpStatusCode.OK, booksResponse.StatusCode);

            var books = await booksResponse.Content.ReadAsAsync<List<BookViewModel>>();
            Assert.NotNull(books);

            var dimensionsResponse = await _httpClient.GetAsync("dimensions");
            Assert.Equal(HttpStatusCode.OK, dimensionsResponse.StatusCode);

            var dimensions = await dimensionsResponse.Content.ReadAsAsync<List<DimensionViewModel>>();
            Assert.NotNull(dimensions);

            var publishersResponse = await _httpClient.GetAsync("publishers");
            Assert.Equal(HttpStatusCode.OK, publishersResponse.StatusCode);

            var publishers = await publishersResponse.Content.ReadAsAsync<List<PublisherViewModel>>();
            Assert.NotNull(publishers);

            var response = await _httpClient.PostAsJsonAsync($"books/{books.First().Id}/editions", new AddEditionCommand
            {
                Isbn = "0123456789111",
                PrintDate = DateTime.Now,
                PageCount = short.MaxValue,
                Id = books.First().Id,
                DimensionsId = dimensions.First().Id,
                PublishersId = publishers.First().Id
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.PutAsJsonAsync($"books/{Guid.NewGuid()}", default(Author));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var booksResponse = await _httpClient.GetAsync("books");
            var books = await booksResponse.Content.ReadAsAsync<List<BookViewModel>>();

            var response = await _httpClient.PutAsJsonAsync($"books/{books.First().Id}", default(Book));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            response = await _httpClient.PutAsJsonAsync($"books/{books.First().Id}", new Book
            {
                Title = "",
                Description = null
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithCorrectData_ShouldReturn_OK()
        {
            var booksResponse = await _httpClient.GetAsync("books");
            var books = await booksResponse.Content.ReadAsAsync<List<BookViewModel>>();

            var response = await _httpClient.PutAsJsonAsync($"books/{books.Last().Id}", new
            {
                Title = "Title",
                Description = "Description",
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task List_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.GetAsync("books?page=-1&pageSize=-5");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task List_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            var response = await _httpClient.GetAsync("books");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseData = await response.Content.ReadAsAsync<List<BookViewModel>>();
            Assert.NotEmpty(responseData);
        }

        [Fact]
        public async Task ListEdition_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            var booksResponse = await _httpClient.GetAsync("books");
            Assert.Equal(HttpStatusCode.OK, booksResponse.StatusCode);

            var books = await booksResponse.Content.ReadAsAsync<List<BookViewModel>>();
            Assert.NotNull(books);

            var response = await _httpClient.GetAsync($"books/{books.First().Id}/editions?page=-1&pageSize=-5");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ListEdition_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            var booksResponse = await _httpClient.GetAsync("books");
            Assert.Equal(HttpStatusCode.OK, booksResponse.StatusCode);

            var books = await booksResponse.Content.ReadAsAsync<List<BookViewModel>>();
            Assert.NotNull(books);

            var response = await _httpClient.GetAsync($"books/{books.First().Id}/editions");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseData = await response.Content.ReadAsAsync<List<BookEditionViewModel>>();
            Assert.NotEmpty(responseData);
        }

        [Fact]
        public async Task Details_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.GetAsync($"books/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Details_WithCorrectId_ShouldReturn_BookObject()
        {
            var booksResponse = await _httpClient.GetAsync("books");
            Assert.Equal(HttpStatusCode.OK, booksResponse.StatusCode);

            var books = await booksResponse.Content.ReadAsAsync<List<BookViewModel>>();
            Assert.NotNull(books);

            var response = await _httpClient.GetAsync($"books/{books.First().Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseData = await response.Content.ReadAsAsync<BookViewModel>();
            Assert.NotNull(responseData);
        }

        [Fact]
        public async Task DetailsEdition_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.GetAsync($"books/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DetailsEdition_WithCorrectId_ShouldReturn_BookEditionObject()
        {
            var booksResponse = await _httpClient.GetAsync("books");
            Assert.Equal(HttpStatusCode.OK, booksResponse.StatusCode);

            var books = await booksResponse.Content.ReadAsAsync<List<BookViewModel>>();
            Assert.NotNull(books);

            var editionsResponse = await _httpClient.GetAsync($"books/{books.First().Id}/editions");
            Assert.Equal(HttpStatusCode.OK, editionsResponse.StatusCode);

            var editions = await editionsResponse.Content.ReadAsAsync<List<BookEditionViewModel>>();
            Assert.NotNull(editions);

            var response = await _httpClient.GetAsync($"books/{books.First().Id}/editions/{editions.First().Isbn}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseData = await response.Content.ReadAsAsync<BookEditionViewModel>();
            Assert.NotNull(responseData);
        }

        [Fact]
        public async Task DeleteOne_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.DeleteAsync($"books/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOne_WithCorrectId_ShouldReturn_OK()
        {
            var booksResponse = await _httpClient.GetAsync("books");
            var books = await booksResponse.Content.ReadAsAsync<List<BookViewModel>>();

            var response = await _httpClient.DeleteAsync($"books/{books.Last().Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOneEdition_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var booksResponse = await _httpClient.GetAsync("books");
            Assert.Equal(HttpStatusCode.OK, booksResponse.StatusCode);

            var books = await booksResponse.Content.ReadAsAsync<List<BookViewModel>>();
            Assert.NotNull(books);

            var response = await _httpClient.DeleteAsync($"books/{books.First().Id}/editions/1");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOneEdition_WithCorrectId_ShouldReturn_OK()
        {
            var booksResponse = await _httpClient.GetAsync("books");
            Assert.Equal(HttpStatusCode.OK, booksResponse.StatusCode);

            var books = await booksResponse.Content.ReadAsAsync<List<BookViewModel>>();
            Assert.NotNull(books);

            var editionsResponse = await _httpClient.GetAsync($"books/{books.First().Id}/editions");
            Assert.Equal(HttpStatusCode.OK, editionsResponse.StatusCode);

            var editions = await editionsResponse.Content.ReadAsAsync<List<BookEditionViewModel>>();
            Assert.NotNull(editions);

            var response = await _httpClient.DeleteAsync($"books/{books.First().Id}/editions/{editions.First().Isbn}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMany_WithoutCorrectIds_ShouldReturn_BadRequest()
        {
            string[] ids = { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress + "books"),
                Content = new StringContent(JsonConvert.SerializeObject(ids), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMany_WithCorrectIds_ShouldReturn_OK()
        {
            var booksResponse = await _httpClient.GetAsync("books");
            var books = await booksResponse.Content.ReadAsAsync<List<BookViewModel>>();

            var dmc = new DeleteManyCommand { Ids = books.TakeLast(2).Select(g => g.Id).ToArray() };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress + "books"),
                Content = new StringContent(JsonConvert.SerializeObject(dmc), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteManyEdition_WithoutCorrectIds_ShouldReturn_BadRequest()
        {
            string[] isbns = { Guid.NewGuid().ToString(), "" };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress + "books"),
                Content = new StringContent(JsonConvert.SerializeObject(isbns), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteManyEdition_WithCorrectIds_ShouldReturn_OK()
        {
            var booksResponse = await _httpClient.GetAsync("books");
            Assert.Equal(HttpStatusCode.OK, booksResponse.StatusCode);

            var books = await booksResponse.Content.ReadAsAsync<List<BookViewModel>>();
            Assert.NotNull(books);

            var editionsResponse = await _httpClient.GetAsync($"books/{books.First().Id}/editions");
            Assert.Equal(HttpStatusCode.OK, editionsResponse.StatusCode);

            var editions = await editionsResponse.Content.ReadAsAsync<List<BookEditionViewModel>>();
            Assert.NotNull(editions);

            var dmec = new DeleteManyEditionCommand { Isbns = editions.Select(e => e.Isbn).ToArray() };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress + "books"),
                Content = new StringContent(JsonConvert.SerializeObject(dmec), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
