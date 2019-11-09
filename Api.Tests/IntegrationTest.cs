using System.Net.Http;
using Cemiyet.Persistence.Application.Contexts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Cemiyet.Api.Tests
{
    public abstract class IntegrationTest
    {
        protected readonly HttpClient _httpClient;

        protected IntegrationTest()
        {
            var applicationFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(AppDataContext));
                    services.AddDbContext<AppDataContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDatabase");
                    });
                });
            });

            _httpClient = applicationFactory.CreateClient();
        }

        // TODO (v0.4)
        /*private async Task AuthtenticateAsync()
        {
        }*/

        // TODO (v0.4)
        /*private async Task RegisterAsync()
        {
        }*/
    }
}
