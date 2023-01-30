using Booking.ApplicationCore.Enum;

namespace Booking.ApplicationCore.Response
{
    public class BaseResponse
    {
        public string? Description { get; set; }

        public StatusCode? StatusCode { get; set; }

        public string? Data { get; set; }

        public BaseResponse(string? description = null, 
            StatusCode? statusCode = null, string? data = null)
        { 
            Description = description;
            StatusCode = statusCode;
            Data = data;
        }
    }
}
