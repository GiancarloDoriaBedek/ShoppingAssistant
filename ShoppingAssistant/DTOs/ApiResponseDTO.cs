using System.Net;

namespace ShoppingAssistant.DTOs
{
    public class ApiResponseDTO
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }

        public ApiResponseDTO()
        {
            ErrorMessages = new List<string>();
            IsSuccess = true;
        }
    }
}
