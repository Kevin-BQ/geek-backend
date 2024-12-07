using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Nombre debe ser minimo 1 maximo 50 caracteres")]
        public string NameProduct { get; set; }

        [Required(ErrorMessage = "Descripcion requerida")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Descripcion detallada requerida")]
        public string LargeDescription { get; set; }

        [Required(ErrorMessage ="Precio requerido")]
        public decimal Price { get; set; }

        public int? Stock { get; set; }

        public bool Status { get; set; }


        [Required(ErrorMessage = "Marca Requerida")]
        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }


        [Required(ErrorMessage = "Categoria Requerida")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }


        [Required(ErrorMessage = "Subcategoria Requerida")]
        public int SubCategoryId { get; set; }

        [ForeignKey("SubCategoryId")]
        public Subcategory Subcategory { get; set; }

        public ICollection<Image>? Images { get; set; }

        public decimal? Discount { get; set; }
    }
}
