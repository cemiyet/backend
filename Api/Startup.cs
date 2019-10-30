using Cemiyet.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cemiyet.Api
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private IConfigurationSection P { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            P = Configuration.GetSection("Project");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MainDataContext>(options =>
            {
                options.UseNpgsql(
                    Configuration.GetConnectionString("MainDataContext"));
            });

            services.AddControllers();

            services.AddMediatR(
                typeof(Application.Genres.Queries.List.ListQuery).Assembly);

            services.AddOpenApiDocument(options =>
            {
                options.Title = P["Name"];
                options.Version = P["Version"];
                options.DocumentName = options.Version;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices
                .GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider
                    .GetRequiredService<MainDataContext>();
                context.Database.Migrate();
                MainDataContextSeed.Seed(context);
            }

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseOpenApi(options => options.Path = "/docs/{documentName}/openapi.json");
            app.UseReDoc(options =>
            {
                options.Path = "/docs";
                options.DocumentPath = "/docs/{documentName}/openapi.json";
            });
        }
    }
}
