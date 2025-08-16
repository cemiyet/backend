using Cemiyet.Modules.Identity.Domain;
using Cemiyet.Modules.Identity.Domain.Repositories;
using Cemiyet.Modules.Identity.Domain.Services;
using Cemiyet.Modules.Identity.Infrastructure.Data;
using Cemiyet.Modules.Identity.Infrastructure.Repositories;
using Cemiyet.Modules.Identity.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cemiyet.Modules.Identity.Infrastructure.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(ModuleConstants.ConnectionStringName), b =>
            {
                b.MigrationsHistoryTable(ModuleConstants.MigrationsHistoryTable, ModuleConstants.SchemaName);
            });
        });

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        // Domain services
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        // Application handlers
        // services.AddScoped<ICommandHandler<RegisterUserCommand, Guid>, RegisterUserCommandHandler>();
        // services.AddScoped<IQueryHandler<GetUserByEmailQuery, UserDto>, GetUserByEmailQueryHandler>();

        return services;
    }
}
