namespace Liara.Common.Abstractions.Cqrs;

public class PaginableQuery<TQuery>
{
    public int? Offset { get; set; }
    public int? Limit { get; set; }
    public TQuery Query { get; set; } = default!;
}
