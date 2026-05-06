namespace Liara.Common.Abstractions;

public interface IResult<TResult> : IResult
{
    TResult Value { get; set; }
}
