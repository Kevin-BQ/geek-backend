using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class BrandDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nombre debe ser Minimo 1 Maximo 50 caracteres")]
        public string NameBrand { get; set; }

        public IFormFile? ImageUrl { get; set; }

        public int? Status { get; set; }
    }
}
