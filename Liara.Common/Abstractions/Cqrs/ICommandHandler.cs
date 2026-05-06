namespace Liara.Common.Abstractions.Cqrs;

public interface ICommandHandler<TCommand, TResult>
{
    Task<IResult<TResult>> ExecuteAsync(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<TCommand>
{
    Task<IResult> ExecuteAsync(TCommand command, CancellationToken cancellationToken);
}