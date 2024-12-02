using Microsoft.AspNetCore.Http;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class ImageDto
    {
        public int? Id { get; set; }
        public IFormFile UrlImage { get; set; }

        [Required(ErrorMessage = "Producto Requerida")]
        public int ProductId { get; set; }
        public string? NameProduct { get; set; }
    }
}
