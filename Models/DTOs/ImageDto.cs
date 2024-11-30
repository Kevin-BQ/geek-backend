using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class ImageDto
    {
        public int Id { get; set; }
        public IFormFile UrlImage { get; set; }
    }
}
