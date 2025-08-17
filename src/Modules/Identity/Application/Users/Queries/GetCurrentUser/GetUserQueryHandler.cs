using Cemiyet.Modules.Identity.Domain.Entities;
using Cemiyet.Modules.Identity.Domain.Repositories;
using Cemiyet.SharedKernel.Application.Queries;

namespace Cemiyet.Modules.Identity.Application.Users.Queries.GetCurrentUser;

public sealed class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> HandleAsync(GetUserQuery query, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.FindByIdAsync(query.UserId, cancellationToken);
        if (user is null)
            throw new KeyNotFoundException("User not found");

        return new UserDto(
            user.Id,
            user.Email.Address,
            user.DisplayName ?? string.Empty
        );
    }
}
