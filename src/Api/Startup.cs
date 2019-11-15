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
            services.AddDbContext<AppDataContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("MainDataContext"));
            });

            services.AddControllers().AddFluentValidation(configuration =>
            {
                configuration.RegisterValidatorsFromAssembly(typeof(ListQuery).Assembly);
            });

            services.AddMediatR(typeof(ListQuery).Assembly);

            services.AddOpenApiDocument(options =>
            {
                options.Title = P["Name"];
                options.Version = P["Version"];
                options.DocumentName = options.Version;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDataContext>();
                context.Database.Migrate();
                AppDataContextSeed.Seed(context);
            }

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
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
