namespace Code.Kata._9.Api.Responses;

public class PaginatedResult<T>(ICollection<T> data, int pageSize, int page)
{
    public ICollection<T> Data { get; } = data;
    public int PageSize { get; } = pageSize;
    public int Page { get; } = page;
}