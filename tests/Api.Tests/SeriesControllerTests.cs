using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cemiyet.Api.Tests.Extensions;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Cemiyet.Api.Tests
{
    public class SeriesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public SeriesControllerTests(WebApplicationFactory<Startup> webApplicationFactory)
        {
            using var scope = webApplicationFactory.Services.GetService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDataContext>();

            AppDataContextSeed.Seed(context);

            _httpClient = webApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task List_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            await _httpClient.AssertedGetAsync("series?page=-1&pageSize=-5", HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task List_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            await _httpClient.AssertedGetEntityListFromUri<AuthorViewModel>("series");
        }
    }
}
