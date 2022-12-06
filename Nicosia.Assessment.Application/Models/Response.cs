
namespace Nicosia.Assessment.Application.Models;

public class Response
{
    public Response()
    {
    }

    public Response(ResponseMeta meta)
    {
        Meta = meta;
    }

    public ResponseMeta Meta { get; set; } = null!;
}

public class Response<T> : Response
{
    public Response()
    {
    }

    public Response(T data) : base(new ResponseMeta())
    {
        Data = data;
    }

    public T Data { get; set; } = default!;
}