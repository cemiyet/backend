using Cemiyet.Modules.Identity.Domain.Services;
using Cemiyet.Modules.Identity.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cemiyet.Modules.Identity.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        // Domain services
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
