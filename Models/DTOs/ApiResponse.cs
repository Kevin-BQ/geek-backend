using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class ApiResponse
    {
        public HttpStatusCode? statusCode { get; set; }
        public bool? IsSuccessful { get; set; }
        public string? Message {  get; set; }
        public object? Result { get; set; }
    }
}
