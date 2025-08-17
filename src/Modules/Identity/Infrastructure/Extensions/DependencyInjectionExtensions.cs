using Cemiyet.Modules.Identity.Application.Services;
using Cemiyet.Modules.Identity.Application.Users.Commands.LoginUser;
using Cemiyet.Modules.Identity.Application.Users.Commands.RefreshToken;
using Cemiyet.Modules.Identity.Application.Users.Commands.RegisterUser;
using Cemiyet.Modules.Identity.Application.Users.Queries.GetCurrentUser;
using Cemiyet.Modules.Identity.Domain;
using Cemiyet.Modules.Identity.Domain.Repositories;
using Cemiyet.Modules.Identity.Domain.Services;
using Cemiyet.Modules.Identity.Infrastructure.Authentication;
using Cemiyet.Modules.Identity.Infrastructure.Data;
using Cemiyet.Modules.Identity.Infrastructure.Repositories;
using Cemiyet.Modules.Identity.Infrastructure.Services;
using Cemiyet.SharedKernel.Application.Commands;
using Cemiyet.SharedKernel.Application.Queries;
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

        // Services
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();

        // Handlers
        services.AddScoped<ICommandHandler<RegisterUserCommand, Guid>, RegisterUserCommandHandler>();
        services.AddScoped<ICommandHandler<LoginUserCommand, LoginResultDto>, LoginUserCommandHandler>();
        services.AddScoped<ICommandHandler<RefreshTokenCommand, LoginResultDto>, RefreshTokenCommandHandler>();

        services.AddScoped<IQueryHandler<GetUserQuery, UserDto>, GetUserQueryHandler>();

        return services;
    }
}
