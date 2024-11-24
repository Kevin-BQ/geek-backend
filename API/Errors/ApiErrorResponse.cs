using System.Dynamic;

namespace API.Errores
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int statusCode, string mensaje=null)
        {
            StatusCode = statusCode;
            Mensaje = mensaje ?? GetMensajeStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Mensaje { get; set; }

        private string GetMensajeStatusCode(int statusCode)
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
