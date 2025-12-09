using System.Dynamic;

namespace API.Errors
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int statusCode, string message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Solicitud No Valida",
                401 => "No Autorizado",
                404 => "No Encontrado",
                500 => "Error Interno del Servidor",
                _ => null
            };
        }
    }
}
