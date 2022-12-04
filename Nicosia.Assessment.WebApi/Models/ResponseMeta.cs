
namespace Nicosia.Assessment.WebApi.Models;

public class ResponseMeta
{
    public int Code { get; set; }
    public string Message { get; set; }
    public MessageType MessageType { get; set; }
}