using Liara.Common.Abstractions;

namespace Liara.Common;

public class ValidationError : IValidationError
{
    public int? ErrorNumber { get; set; }
    public string? RuleOrFieldName { get; set; }
    public string ErrorMessage { get; set; } = default!;    
}
