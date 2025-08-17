using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Cemiyet.Modules.Identity.Application.Users.Commands.LoginUser;
using Cemiyet.Modules.Identity.Application.Users.Commands.RefreshToken;
using Cemiyet.Modules.Identity.Application.Users.Commands.RegisterUser;
using Cemiyet.Modules.Identity.Application.Users.Queries.GetCurrentUser;
using Cemiyet.SharedKernel.Application.Dispatching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Gateway.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public UsersController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(request.Email, request.Password);
        Guid userId = await _dispatcher.DispatchAsync(command, cancellationToken);
        return Ok(userId);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(request.Email, request.Password);
        LoginResultDto loginResult = await _dispatcher.DispatchAsync(command, cancellationToken);
        return Ok(loginResult);
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var command = new RefreshTokenCommand(request.RefreshToken);
        LoginResultDto refreshTokenResult = await _dispatcher.DispatchAsync(command, cancellationToken);
        return Ok(refreshTokenResult);
    }

    [HttpGet("GetUser/{userId}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetUserQuery(userId);
        UserDto result = await _dispatcher.DispatchAsync(query, cancellationToken);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("Me")]
    public async Task<IActionResult> GetMe(CancellationToken cancellationToken)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                       ?? User.FindFirst(JwtRegisteredClaimNames.Sub);

        if (userIdClaim == null)
            return Unauthorized();

        Guid userId = Guid.Parse(userIdClaim.Value);
        var query = new GetUserQuery(userId);
        UserDto result = await _dispatcher.DispatchAsync(query, cancellationToken);
        return Ok(result);
    }

    public sealed record RegisterUserRequest(string Email, string Password);
    public sealed record LoginUserRequest(string Email, string Password);
    public sealed record RefreshTokenRequest(string RefreshToken);
}
