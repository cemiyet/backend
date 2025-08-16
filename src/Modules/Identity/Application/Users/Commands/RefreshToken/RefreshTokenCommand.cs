using Cemiyet.Modules.Identity.Application.Users.Commands.LoginUser;
using Cemiyet.SharedKernel.Application.Commands;

namespace Cemiyet.Modules.Identity.Application.Users.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : ICommand<LoginResultDto>;
