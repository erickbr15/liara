namespace Liara.Common.Abstractions;

public interface IResult
{
    bool IsSuccess { get; set; }
    bool HasErrors { get; }
    IEnumerable<Exception> Exceptions { get; }
    IEnumerable<IValidationError> ValidationErrors { get; }
    bool AnyExceptions { get; }
    bool AnyValidationErrors { get; }
}
