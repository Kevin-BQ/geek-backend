namespace API.Errors
{
    public class ApiValidationErrorResponse: ApiErrorResponse
    {
        public ApiValidationErrorResponse(): base(400)
        {
            
        }

        public Dictionary<string, string> Errors { get; set; }

        public static string AttributeToCamelCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return char.ToLowerInvariant(input[0]) + input.Substring(1);
        }
    }
}
