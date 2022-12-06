
namespace Nicosia.Assessment.Application.Models;

public class PaginationFilter : Filter
{
    public int Offset { get; set; }
    public int Count { get; set; }
}