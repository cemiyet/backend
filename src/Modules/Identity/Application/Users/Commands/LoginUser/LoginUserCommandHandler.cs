using Cemiyet.Modules.Identity.Application.Services;
using Cemiyet.Modules.Identity.Domain.Entities;
using Cemiyet.Modules.Identity.Domain.Repositories;
using Cemiyet.Modules.Identity.Domain.Services;
using Cemiyet.Modules.Identity.Domain.ValueObjects;
using Cemiyet.SharedKernel.Application.Commands;

namespace Cemiyet.Modules.Identity.Application.Users.Commands.LoginUser;

public sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, LoginResultDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<LoginResultDto> HandleAsync(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var email = new Email(command.Email);
        User? user = await _userRepository.FindByEmailAsync(email, cancellationToken);

        if (user is null || !_passwordHasher.Verify(command.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        AccessToken accessToken = _tokenService.GenerateAccessToken(user);
        string refreshToken = _tokenService.GenerateRefreshToken(user);

        user.SetRefreshToken(refreshToken);

        await _userRepository.UpdateAsync(user, cancellationToken);

        return new LoginResultDto(
            accessToken.Token,
            refreshToken,
            accessToken.ExpiresAt
        );
    }
}

