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
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nombre debe ser Minimo 1 Maximo 50 caracteres")]
        public string NameSubcategory { get; set; }

        public int Estatus { get; set; }

        [Required(ErrorMessage = "Especialida Requerida")]
        public int CategoryId { get; set; }

        public string? NameCategory { get; set; }
    }
}
