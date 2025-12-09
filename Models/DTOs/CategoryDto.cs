using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nombre debe ser minimo 1 maximo 50 caracteres")]
        public string NameCategory { get; set; }

        public int Status { get; set; }
    }
}
