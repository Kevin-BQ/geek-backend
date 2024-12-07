using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nombre debe ser Minimo 1 Maximo 50 caracteres")]
        public string NameCategory { get; set; }

        public bool Estatus { get; set; }
        public bool Status { get; set; }
    }
}
