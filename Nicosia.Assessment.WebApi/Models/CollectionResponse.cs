using System.Collections.Generic;

namespace Nicosia.Assessment.WebApi.Models;

public class CollectionResponse<T>
{
    public CollectionResponse()
    {
    }

    public CollectionResponse(IEnumerable<T> items, long totalCount, string nextPageUrl)
    {
        Items = items;
        TotalCount = totalCount;
        NextPageUrl = nextPageUrl;
    }

    public IEnumerable<T> Items { get; set; } = null!;
    public long TotalCount { get; set; }
    public string NextPageUrl { get; set; } = null!;
}