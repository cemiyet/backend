namespace Cemiyet.SharedKernel.Application.Queries;

/// <summary>
/// Handler for queries of type TQuery producing TResult.
/// </summary>
public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
}
