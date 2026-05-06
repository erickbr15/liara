using Liara.Common.Abstractions;

namespace Liara.Common;

public sealed class Result<TResult> : IResult<TResult>
{    
    public bool IsSuccess { get; set; }
    public bool HasErrors => AnyExceptions || AnyValidationErrors;

    public TResult Value { get; set; } = default!;
    public IEnumerable<Exception> Exceptions { get; private set; } = new List<Exception>();
    public IEnumerable<IValidationError> ValidationErrors { get; private set; } = new List<ValidationError>();

    public bool AnyExceptions => Exceptions.Any();
    public bool AnyValidationErrors => ValidationErrors.Any();

    public Result()
    {
        IsSuccess = false;
    }

    public Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public Result(bool isSuccess, IEnumerable<IValidationError> validationErrors, IEnumerable<Exception> errors)
        : this(isSuccess)
    {
        if (errors?.Any() ?? false)
        {
            (this.Exceptions as List<Exception>)!.AddRange(errors);
        }

        if (validationErrors?.Any() ?? false)
        {
            (this.ValidationErrors as List<IValidationError>)!.AddRange(validationErrors);
        }
    }

    public static IResult<TResult> Success(TResult value) => new Result<TResult>(isSuccess: true) { Value = value };

    public static IResult<TResult> Fail() => new Result<TResult>(isSuccess: false);

    public static IResult<TResult> Fail(IValidationError validationError) =>
       new Result<TResult>(false, new List<IValidationError> { validationError }, Enumerable.Empty<Exception>());

    public static IResult<TResult> Fail(IEnumerable<IValidationError> validationErrors) =>
        new Result<TResult>(false, validationErrors, Enumerable.Empty<Exception>());

    public static IResult<TResult> Fail(Exception error) =>
        new Result<TResult>(false, Enumerable.Empty<IValidationError>(), new List<Exception> { error });

    public static IResult<TResult> Fail(IEnumerable<Exception> errors) =>
        new Result<TResult>(false, Enumerable.Empty<IValidationError>(), errors);

    public static IResult<TResult> Fail(IEnumerable<IValidationError> validationErrors, IEnumerable<Exception> errors) =>
        new Result<TResult>(false, validationErrors, errors);    
}