namespace Site.Application.Definitions.Responses
{
    public class BaseResponse<T>
    {
        public BaseResponse(bool success, string message, T data, List<string> errors)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors ?? new List<string>();
        }
        public BaseResponse()
        {
            Success = true;
            Message = string.Empty;
            Data = default(T);
            Errors = new List<string>();
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
    }
    public class BaseResponse
    {
        public BaseResponse(bool success, string message, List<string> errors)
        {
            Success = success;
            Message = message;
            Errors = errors ?? new List<string>();
        }
        public BaseResponse()
        {
            Success = true;
            Message = string.Empty;
            Errors = new List<string>();
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
