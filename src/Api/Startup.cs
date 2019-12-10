using Cemiyet.Application.Genres.Queries.List;
using Cemiyet.Persistence.Application.Contexts;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Cemiyet.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(configuration =>
            {
                configuration.RegisterValidatorsFromAssembly(typeof(ListQuery).Assembly);
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddDbContext<AppDataContext>(options =>
            {
                if (_environment.IsEnvironment("Test"))
                {
                    options.UseLazyLoadingProxies().UseInMemoryDatabase("TestDatabase");
                }
                else
                {
                    options.UseLazyLoadingProxies().UseNpgsql(_configuration.GetConnectionString("MainDataContext"));
                }
            });

            services.AddMediatR(typeof(ListQuery).Assembly);

            services.AddOpenApiDocument(options =>
            {
                options.Title = _configuration.GetSection("Project")["Name"];
                options.Version = _configuration.GetSection("Project")["Version"];
                options.DocumentName = options.Version;
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDataContext>();

                if (_environment.IsEnvironment("Test"))
                {
                    context.Database.EnsureDeleted();
                }
                else
                {
                    context.Database.Migrate();
                }

                AppDataContextSeed.Seed(context);
            }

            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (_environment.IsProduction())
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseOpenApi(options => options.Path = "/docs/{documentName}/openapi.json");

            app.UseSwaggerUi3(options =>
            {
                options.Path = "/docs";
                options.DocumentPath = "/docs/{documentName}/openapi.json";
            });
        }
    }
}
