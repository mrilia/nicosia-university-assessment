namespace Nicosia.Assessment.AcceptanceTests.Models.PaginationResponse;

public class PaginationResponse<T> : Response<CollectionResponse<T>>
{
    public PaginationResponse()
    {
    }

    public PaginationResponse(CollectionResponse<T> items) : base(items)
    {
    }

    public PaginationResponse(IEnumerable<T> items, long totalCount, string nextPageUrl) : base(new CollectionResponse<T>(items, totalCount, nextPageUrl))
    {
    }
}