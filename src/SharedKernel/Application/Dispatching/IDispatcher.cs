using Cemiyet.SharedKernel.Application.Commands;
using Cemiyet.SharedKernel.Application.Queries;

namespace Cemiyet.SharedKernel.Application.Dispatching;

public interface IDispatcher
{
    Task<TResult> DispatchAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken);
    Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken);
}
