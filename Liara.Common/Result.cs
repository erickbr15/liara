using Liara.Common.Abstractions;

namespace Liara.Common;

public sealed class Result : IResult
{   
    public bool IsSuccess { get; set; }
    public bool HasErrors => AnyExceptions || AnyValidationErrors;

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
        :this(isSuccess)
    {
        if (errors?.Any() ?? false)
        {
            (this.Exceptions as List<Exception>)!.AddRange(errors);
        }

        if(validationErrors?.Any() ?? false)
        {
            (this.ValidationErrors as List<IValidationError>)!.AddRange(validationErrors);
        }
    }

    public static IResult Create(bool isSuccess, IEnumerable<IValidationError> validationErrors, IEnumerable<Exception> errors) =>
        new Result(isSuccess, validationErrors, errors);

    public static IResult Success() => new Result(isSuccess: true);    

    public static IResult Fail() => new Result(isSuccess: false);

    public static IResult Fail(IValidationError validationError) =>
        new Result(false, new List<IValidationError> { validationError }, Enumerable.Empty<Exception>());

    public static IResult Fail(IEnumerable<IValidationError> validationErrors) =>
        new Result(false, validationErrors, Enumerable.Empty<Exception>());

    public static IResult Fail(Exception error) =>
        new Result(false, Enumerable.Empty<IValidationError>(), new List<Exception> { error });

    public static IResult Fail(IEnumerable<Exception> errors) =>
        new Result(false, Enumerable.Empty<IValidationError>(), errors);

    public static IResult Fail(IEnumerable<IValidationError> validationErrors, IEnumerable<Exception> errors) =>
        new Result(false, validationErrors, errors);
}