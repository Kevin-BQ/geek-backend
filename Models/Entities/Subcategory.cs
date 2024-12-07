using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Subcategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nombre debe ser minimo 1 maximo 50 caracteres")]
        public string NameSubcategory { get; set; }

        public bool Status { get; set; }

        [Required(ErrorMessage = "Categoria requerida")]
        public int CategoryId { get; set; }


        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

    }
}
