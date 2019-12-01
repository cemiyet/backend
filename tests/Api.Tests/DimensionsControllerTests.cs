using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cemiyet.Api.Tests.Extensions;
using Cemiyet.Application.Dimensions.Commands.DeleteMany;
using Cemiyet.Core.Entities;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Cemiyet.Api.Tests
{
    public class DimensionsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public DimensionsControllerTests(WebApplicationFactory<Startup> webApplicationFactory)
        {
            using var scope = webApplicationFactory.Services.GetService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDataContext>();

            AppDataContextSeed.Seed(context);

            _httpClient = webApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task Add_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.PostAsJsonAsync("dimensions/", default(Dimension));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Add_WithCorrectData_ShouldReturn_OK()
        {
            var response = await _httpClient.PostAsJsonAsync("dimensions/", new Dimension
            {
                Width = 1.5,
                Height = 2.5
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task List_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            await _httpClient.AssertedGetAsync("dimensions?page=-1&pageSize=-5", HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task List_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            await _httpClient.AssertedGetEntityListFromUri<DimensionViewModel>("dimensions");
        }

        [Fact]
        public async Task Details_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            await _httpClient.AssertedGetAsync($"dimensions/{Guid.NewGuid()}", HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Details_WithCorrectId_ShouldReturn_DimensionObject()
        {
            var dimensions = await _httpClient.AssertedGetEntityListFromUri<DimensionViewModel>("dimensions");
            var response = await _httpClient.AssertedGetAsync($"dimensions/{dimensions.First().Id}", HttpStatusCode.OK);
            var responseData = await response.Content.ReadAsAsync<DimensionViewModel>();
            Assert.NotNull(responseData);
        }

        [Fact]
        public async Task Update_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.PutAsJsonAsync($"dimensions/{Guid.NewGuid()}", default(Dimension));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var dimensions = await _httpClient.AssertedGetEntityListFromUri<DimensionViewModel>("dimensions");
            var response = await _httpClient.PutAsJsonAsync($"dimensions/{dimensions.First().Id}", default(Dimension));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            response = await _httpClient.PutAsJsonAsync($"dimensions/{dimensions.First().Id}", new
            {
                Width = 5.0
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithCorrectData_ShouldReturn_OK()
        {
            var dimensions = await _httpClient.AssertedGetEntityListFromUri<DimensionViewModel>("dimensions");
            var response = await _httpClient.PutAsJsonAsync($"dimensions/{dimensions.Last().Id}", new
            {
                Width = 1.2,
                Height = 1.6
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePartially_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var dimensions = await _httpClient.AssertedGetEntityListFromUri<DimensionViewModel>("dimensions");
            await _httpClient.AssertedSendRequestMessageAsync(HttpMethod.Patch, $"dimensions/{dimensions.First().Id}",
                                                              new { }, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdatePartially_WithCorrectData_ShouldReturn_OK()
        {
            var dimensions = await _httpClient.AssertedGetEntityListFromUri<DimensionViewModel>("dimensions");
            await _httpClient.AssertedSendRequestMessageAsync(HttpMethod.Patch, $"dimensions/{dimensions.First().Id}",
                                                              new { Width = 5.0 }, HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteOne_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.DeleteAsync($"dimensions/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOne_WithCorrectId_ShouldReturn_OK()
        {
            var dimensions = await _httpClient.AssertedGetEntityListFromUri<DimensionViewModel>("dimensions");
            var response = await _httpClient.DeleteAsync($"dimensions/{dimensions.Last().Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMany_WithoutCorrectIds_ShouldReturn_BadRequest()
        {
            await _httpClient.AssertedSendRequestMessageAsync(HttpMethod.Delete, "dimensions", new []
            {
                Guid.NewGuid().ToString(), Guid.NewGuid().ToString()
            }, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteMany_WithCorrectIds_ShouldReturn_OK()
        {
            var dimensions = await _httpClient.AssertedGetEntityListFromUri<DimensionViewModel>("dimensions");
            await _httpClient.AssertedSendRequestMessageAsync(HttpMethod.Delete, "dimensions", new DeleteManyCommand
            {
                Ids = dimensions.TakeLast(2).Select(g => g.Id).ToArray()
            }, HttpStatusCode.OK);
        }
    }
}
