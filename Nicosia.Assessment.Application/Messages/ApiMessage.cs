

namespace Nicosia.Assessment.Application.Messages
{
    public class ApiMessage
    {
        public ApiMessage()
        {

        }

        public ApiMessage(string message)
        {
            Message = message;
        }
        public string Message { get; set; }

        public string Detail { get; set; }

    }
}
