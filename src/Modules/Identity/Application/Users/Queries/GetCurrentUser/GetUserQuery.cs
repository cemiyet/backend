using Cemiyet.SharedKernel.Application.Queries;

namespace Cemiyet.Modules.Identity.Application.Users.Queries.GetCurrentUser;

public record GetUserQuery(Guid UserId) : IQuery<UserDto>;
