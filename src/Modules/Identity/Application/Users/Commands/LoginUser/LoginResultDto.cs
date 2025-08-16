namespace Cemiyet.Modules.Identity.Application.Users.Commands.LoginUser;

public sealed record LoginResultDto(string AccessToken, string RefreshToken, DateTime ExpiresAt);

