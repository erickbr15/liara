namespace Liara.Common.Abstractions.Cqrs;

public interface IQueryHandler<TQuery, TResult>
{
    Task<IResult<TResult>> ExecuteAsync(TQuery query, CancellationToken cancellationToken);
}

public interface IPaginableQueryHandler<TQuery, TItem>
{
    Task<IPaginationResult<TItem>> ExecuteAsync(PaginableQuery<TQuery> query, CancellationToken cancellationToken);
}
