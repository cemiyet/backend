using Cemiyet.SharedKernel.Application.Commands;

namespace Cemiyet.Modules.Identity.Application.Users.Commands.RegisterUser;

public sealed record RegisterUserCommand(string Email, string Password) : ICommand<Guid>;

