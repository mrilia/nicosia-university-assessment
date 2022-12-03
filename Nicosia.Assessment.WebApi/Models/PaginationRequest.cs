namespace Nicosia.Assessment.WebApi.Models;

public class PaginationRequest
{
    public string? Keyword { get; set; }
    public int Offset { get; set; } = 0;
    public int Count { get; set; } = 20;

    public PaginationFilter ConvertToPaginationFilter()
    {
        return new PaginationFilter
        {
            Keyword = Keyword,
            Offset = Offset,
            Count = Count
        };
    }

    public T ConvertTo<T>() where T : PaginationFilter, new()
    {
        return new T
        {
            Keyword = Keyword,
            Offset = Offset,
            Count = Count
        };
    }
}