namespace Nicosia.Assessment.AcceptanceTests.Models.PaginationResponse;

public class PaginationRequest
{
    public int Offset { get; set; } = 0;
    public int Count { get; set; } = 20;

    public PaginationFilter ConvertToPaginationFilter()
    {
        return new PaginationFilter
        {
            Offset = Offset,
            Count = Count
        };
    }

    public T ConvertTo<T>() where T : PaginationFilter, new()
    {
        return new T
        {
            Offset = Offset,
            Count = Count
        };
    }
}