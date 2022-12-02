using Microsoft.AspNetCore.Mvc;

namespace Nicosia.Assessment.Application.Results
{
    public class Result<T>
    {
        public T Data { get; private set; }
        public string Message { get; set; }

        public ObjectResult ApiResult { get; set; }
        public bool Success { get; set; }

        public static Result<T> SuccessFul(T data) => new() { ApiResult = new OkObjectResult(data), Data = data, Message = null, Success = true };
        public static Result<T> Failed(ObjectResult error) => new() { ApiResult = error, Success = false, Message = error.Value?.ToString() };
    }

    public class Result
    {
        public string Message { get; set; }

        public ObjectResult ApiResult { get; set; }
        public bool Success { get; set; }

        public static Result SuccessFul() => new() { Message = null, Success = true };

        public static Result Failed(ObjectResult error) => new() { ApiResult = error, Success = false, Message = error.Value?.ToString() };
    }
}
