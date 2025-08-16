using System.Security.Claims;
using Cemiyet.Modules.Identity.Application.Services;
using Cemiyet.Modules.Identity.Application.Users.Commands.LoginUser;
using Cemiyet.Modules.Identity.Domain.Entities;
using Cemiyet.Modules.Identity.Domain.Repositories;
using Cemiyet.Modules.Identity.Domain.ValueObjects;
using Cemiyet.SharedKernel.Application.Commands;

namespace Cemiyet.Modules.Identity.Application.Users.Commands.RefreshToken;

public sealed class RefreshTokenHandler : ICommandHandler<RefreshTokenCommand, LoginResultDto>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public RefreshTokenHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<LoginResultDto> HandleAsync(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        ClaimsPrincipal? principal = _tokenService.GetPrincipalFromToken(command.RefreshToken);
        if (principal is null)
            throw new UnauthorizedAccessException("Refresh token expired or invalid");

        User? user = await _userRepository.FindByRefreshTokenAsync(command.RefreshToken, cancellationToken);
        if (user is null)
            throw new UnauthorizedAccessException("Invalid refresh token");

        AccessToken newAccessToken = _tokenService.GenerateAccessToken(user);
        string newRefreshToken = _tokenService.GenerateRefreshToken(user);

        user.SetRefreshToken(newRefreshToken);
        await _userRepository.UpdateAsync(user, cancellationToken);

        return new LoginResultDto(newAccessToken.Token, newRefreshToken, newAccessToken.ExpiresAt);
    }
}
