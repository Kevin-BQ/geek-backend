namespace API.Errors
{
    public class ApiException: ApiErrorResponse
    {
        public ApiException(int statusCode, string message = null, string detalle = null) : base(statusCode, message)
        {
            Detalle = detalle;
        }

        public string Detalle { get; set; }
    }
}
