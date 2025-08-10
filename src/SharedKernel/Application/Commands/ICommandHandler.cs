namespace Cemiyet.SharedKernel.Application.Commands;

/// <summary>
/// Handler for commands of type TCommand producing TResult.
/// </summary>
public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
}
