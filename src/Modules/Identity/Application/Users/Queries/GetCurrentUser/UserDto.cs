namespace Cemiyet.Modules.Identity.Application.Users.Queries.GetCurrentUser;

public sealed record UserDto(Guid Id, string Email, string DisplayName);
