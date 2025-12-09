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
    public class SubcategoryDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nombre debe ser minimo 1 maximo 50 caracteres")]
        public string NameSubcategory { get; set; }

        public int Status { get; set; }

        [Required(ErrorMessage = "Categoria requerida")]
        public int CategoryId { get; set; }

        public string? NameCategory { get; set; }
    }
}
