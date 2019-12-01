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
            var genres = await GetEntityListFromUri<GenreViewModel>("genres?page=1&pageSize=2");
            var authors = await GetEntityListFromUri<AuthorViewModel>("authors?page=1&pageSize=2");

            var response = await _httpClient.PostAsJsonAsync("books/", new AddCommand
            {
                Title = "abc",
                Description = "abcdesc",
                GenreIds = genres.Select(g => g.Id).ToList(),
                AuthorIds = authors.Select(a => a.Id).ToList()
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task AddEdition_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var books = await GetEntityListFromUri<BookViewModel>("books");

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
            var books = await GetEntityListFromUri<BookViewModel>("books");
            var dimensions = await GetEntityListFromUri<DimensionViewModel>("dimensions");
            var publishers = await GetEntityListFromUri<PublisherViewModel>("publishers");

            var response = await _httpClient.PostAsJsonAsync($"books/{books.First().Id}/editions", new AddEditionCommand
            {
                Isbn = "0123456789111",
                PrintDate = DateTime.Now,
                PageCount = short.MaxValue,
                Id = books.First().Id,
                DimensionsId = dimensions.First().Id,
                BooksId = books.First().Id,
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
            var books = await GetEntityListFromUri<BookViewModel>("books");

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
            var books = await GetEntityListFromUri<BookViewModel>("books");
            var response = await _httpClient.PutAsJsonAsync($"books/{books.Last().Id}", new
            {
                Title = "Title",
                Description = "Description",
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UpdateEdition_WithoutCorrectIsbn_ShouldReturn_BadRequest()
        {
            var books = await GetEntityListFromUri<BookViewModel>("books");
            var response = await _httpClient.PutAsJsonAsync($"books/{books.First().Id}/editions/1234567890111", default(BookEdition));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateEdition_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var books = await GetEntityListFromUri<BookViewModel>("books");
            var book = books.First();
            var edition = book.Editions.First();
            var response = await _httpClient.PutAsJsonAsync($"books/{book.Id}/editions/{edition.Isbn}", default(BookEdition));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateEdition_WithCorrectData_ShouldReturn_OK()
        {
            var books = await GetEntityListFromUri<BookViewModel>("books");
            var book = books.First();
            var edition = book.Editions.First();

            var response = await _httpClient.PutAsJsonAsync($"books/{book.Id}/editions/{edition.Isbn}", new
            {
                edition.Isbn,
                edition.PageCount,
                edition.PrintDate,
                BooksId = book.Id,
                DimensionsId = edition.Dimensions.Id,
                PublishersId = edition.Publisher.Id,
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePartially_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var books = await GetEntityListFromUri<BookViewModel>("books");
            var request = CreateRequestMessage(HttpMethod.Patch, $"books/{books.First().Id}", new { });
            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePartially_WithCorrectData_ShouldReturn_OK()
        {
            var books = await GetEntityListFromUri<BookViewModel>("books");
            var request = CreateRequestMessage(HttpMethod.Patch, $"books/{books.First().Id}", new
            {
                Title = "BAŞLIK"
            });
            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePartiallyEdition_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var books = await GetEntityListFromUri<BookViewModel>("books");
            var book = books.First();
            var edition = book.Editions.First();
            var request = CreateRequestMessage(HttpMethod.Patch, $"books/{book.Id}/editions/{edition.Isbn}", new
            {
                Isbn = "1",
                NewIsbn = "",
                BooksId = Guid.Empty
            });
            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePartiallyEdition_WithCorrectData_ShouldReturn_OK()
        {
            var books = await GetEntityListFromUri<BookViewModel>("books");
            var book = books.First();
            var edition = book.Editions.First();
            var request = CreateRequestMessage(HttpMethod.Patch, $"books/{book.Id}/editions/{edition.Isbn}", new
            {
                NewIsbn = "1234567890111",
                PageCount = short.MaxValue
            });
            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task List_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            await GetHttpGetAsyncResponse("books?page=-1&pageSize=-5", HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task List_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            await GetEntityListFromUri<BookViewModel>("books");
        }

        [Fact]
        public async Task ListEdition_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            var books = await GetEntityListFromUri<BookViewModel>("books");
            await GetHttpGetAsyncResponse($"books/{books.First().Id}/editions?page=-1&pageSize=-5", HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ListEdition_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            var books = await GetEntityListFromUri<BookViewModel>("books");
            var response = await GetHttpGetAsyncResponse($"books/{books.First().Id}/editions", HttpStatusCode.OK);
            var responseData = await response.Content.ReadAsAsync<List<BookEditionViewModel>>();
            Assert.NotEmpty(responseData);
        }

        [Fact]
        public async Task Details_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            await GetHttpGetAsyncResponse($"books/{Guid.NewGuid()}", HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Details_WithCorrectId_ShouldReturn_BookObject()
        {
            var books = await GetEntityListFromUri<BookViewModel>("books");
            var response = await GetHttpGetAsyncResponse($"books/{books.First().Id}", HttpStatusCode.OK);
            var responseData = await response.Content.ReadAsAsync<BookViewModel>();
            Assert.NotNull(responseData);
        }

        [Fact]
        public async Task DetailsEdition_WithoutCorrectIsbn_ShouldReturn_BadRequest()
        {
            await GetHttpGetAsyncResponse($"books/{Guid.NewGuid()}/editions/1234567890111", HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DetailsEdition_WithCorrectIsbn_ShouldReturn_BookEditionObject()
        {
            var books = await GetEntityListFromUri<BookViewModel>("books");
            var book = books.First();
            var edition = book.Editions.First();
            var response = await GetHttpGetAsyncResponse($"books/{book.Id}/editions/{edition.Isbn}", HttpStatusCode.OK);
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
            var books = await GetEntityListFromUri<BookViewModel>("books");
            var response = await _httpClient.DeleteAsync($"books/{books.Last().Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOneEdition_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var books = await GetEntityListFromUri<BookViewModel>("books");
            var response = await _httpClient.DeleteAsync($"books/{books.First().Id}/editions/1");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOneEdition_WithCorrectId_ShouldReturn_OK()
        {
            var books = await GetEntityListFromUri<BookViewModel>("books");
            var book = books.First();
            var edition = book.Editions.First();
            var response = await _httpClient.DeleteAsync($"books/{book.Id}/editions/{edition.Isbn}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMany_WithoutCorrectIds_ShouldReturn_BadRequest()
        {
            var request = CreateRequestMessage(HttpMethod.Delete, "books", new[]
            {
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString()
            });
            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMany_WithCorrectIds_ShouldReturn_OK()
        {
            var books = await GetEntityListFromUri<BookViewModel>("books");
            var request = CreateRequestMessage(HttpMethod.Delete, "books", new DeleteManyCommand
            {
                Ids = books.TakeLast(2).Select(g => g.Id).ToArray()
            });
            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteManyEdition_WithoutCorrectIds_ShouldReturn_BadRequest()
        {
            var request = CreateRequestMessage(HttpMethod.Delete, "books", new[]
            {
                Guid.NewGuid().ToString(), ""
            });
            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteManyEdition_WithCorrectIds_ShouldReturn_OK()
        {
            var books = await GetEntityListFromUri<BookViewModel>("books");
            var request = CreateRequestMessage(HttpMethod.Delete, "books", new DeleteManyEditionCommand
            {
                Isbns = books.First().Editions.Select(e => e.Isbn).ToArray()
            });
            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task<List<T>> GetEntityListFromUri<T>(string uri)
        {
            var response = await GetHttpGetAsyncResponse(uri, HttpStatusCode.OK);
            var collection = await response.Content.ReadAsAsync<List<T>>();

            Assert.NotEmpty(collection);

            return collection;
        }

        private async Task<HttpResponseMessage> GetHttpGetAsyncResponse(string uri, HttpStatusCode code)
        {
            var response = await _httpClient.GetAsync(uri);
            Assert.Equal(code, response.StatusCode);

            return response;
        }

        private HttpRequestMessage CreateRequestMessage(HttpMethod method, string uri, object content)
        {
            return new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(_httpClient.BaseAddress + uri),
                Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")
            };
        }
    }
}
