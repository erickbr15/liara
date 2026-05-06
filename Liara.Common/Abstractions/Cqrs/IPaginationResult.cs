namespace Liara.Common.Abstractions.Cqrs;

public interface IPaginationResult<TItem> : IResult
{
    public IEnumerable<TItem> Items { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }

}
