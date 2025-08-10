using Cemiyet.SharedKernel.Application.Commands;
using Cemiyet.SharedKernel.Application.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Cemiyet.SharedKernel.Application.Dispatching;

/// <summary>
/// Dispatcher responsible for sending commands and queries to their respective handlers.
/// Uses IServiceProvider to resolve the handler for a given command or query type.
/// </summary>
public sealed class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Creates a new Dispatcher with the provided service provider for handler resolution.
    /// </summary>
    /// <param name="serviceProvider">Service provider used to resolve command/query handlers.</param>
    public Dispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Dispatches a command to its corresponding handler asynchronously.
    /// </summary>
    /// <typeparam name="TResult">The return type of the command handler.</typeparam>
    /// <param name="command">The command instance to be handled.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result produced by the command handler.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no suitable handler is found.</exception>
    public Task<TResult> DispatchAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken)
    {
        Type handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
        dynamic handler = _serviceProvider.GetRequiredService(handlerType);
        return handler.HandleAsync((dynamic)command, cancellationToken);
    }

    /// <summary>
    /// Dispatches a query to its corresponding handler asynchronously.
    /// </summary>
    /// <typeparam name="TResult">The return type of the query handler.</typeparam>
    /// <param name="query">The query instance to be handled.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result produced by the query handler.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no suitable handler is found.</exception>
    public Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
    {
        Type handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        dynamic handler = _serviceProvider.GetRequiredService(handlerType);
        return handler.HandleAsync((dynamic)query, cancellationToken);
    }
}
