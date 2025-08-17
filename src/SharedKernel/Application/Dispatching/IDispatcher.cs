using Cemiyet.SharedKernel.Application.Commands;
using Cemiyet.SharedKernel.Application.Queries;

namespace Cemiyet.SharedKernel.Application.Dispatching;

public interface IDispatcher
{
    /// <summary>
    /// Dispatches a command to its corresponding handler asynchronously.
    /// </summary>
    /// <typeparam name="TResult">The return type of the command handler.</typeparam>
    /// <param name="command">The command instance to be handled.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result produced by the command handler.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no suitable handler is found.</exception>
    Task<TResult> DispatchAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken);

    /// <summary>
    /// Dispatches a query to its corresponding handler asynchronously.
    /// </summary>
    /// <typeparam name="TResult">The return type of the query handler.</typeparam>
    /// <param name="query">The query instance to be handled.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result produced by the query handler.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no suitable handler is found.</exception>
    Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken);
}
