namespace Liara.Common.Abstractions;

public interface IValidationError
{
    int? ErrorNumber { get; set; }
    string? RuleOrFieldName { get; set; }
    string ErrorMessage { get; set; }
}
