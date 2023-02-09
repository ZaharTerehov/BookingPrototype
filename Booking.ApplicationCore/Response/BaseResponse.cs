using Booking.ApplicationCore.Enum;

namespace Booking.ApplicationCore.Response
{
    public class BaseResponse<T> where T : class
    {
        public string? Description { get; set; }

        public StatusCode? StatusCode { get; set; }

        public T? Data { get; set; }

        public BaseResponse(T? data = null, string? description = null,
            StatusCode? statusCode = null)
        { 
            Description = description;
            StatusCode = statusCode;
            Data = data;
        }
    }
}
