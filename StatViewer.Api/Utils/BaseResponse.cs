namespace StatViewer.Api.Utils
{
    public class BaseResponse 
    {
        public int? Id { get; set; }
        public bool Status { get; set; }
        public string? Message { get; set; } = string.Empty;

        public BaseResponse() { }

        public BaseResponse(int? id, bool status, string? message = null)
        {
            Id = id;
            Status = status;
            Message = message;
        }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }

        public BaseResponse(T data, int? id, bool status, string? message = null)
        {
            Id = id;
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
