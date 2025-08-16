using Cemiyet.SharedKernel.Application.Commands;

namespace Cemiyet.Modules.Identity.Application.Users.Commands.LoginUser;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<LoginResultDto>;

