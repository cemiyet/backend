using Cemiyet.Modules.Identity.Domain.Entities;
using Cemiyet.Modules.Identity.Domain.Repositories;
using Cemiyet.Modules.Identity.Domain.Services;
using Cemiyet.Modules.Identity.Domain.ValueObjects;
using Cemiyet.SharedKernel.Application.Commands;

namespace Cemiyet.Modules.Identity.Application.Users.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> HandleAsync(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var email = new Email(command.Email);

        User? existingUser = await _userRepository.FindByEmailAsync(email, cancellationToken);

        if (existingUser is not null)
            throw new InvalidOperationException("User already exists.");

        string hashedPassword = _passwordHasher.Hash(command.Password);

        User user = User.Register(email, hashedPassword);

        await _userRepository.AddAsync(user, cancellationToken);

        return user.Id;
    }
}

