using Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Models.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre Requerido")]
        public string NameProduct { get; set; }

        [Required(ErrorMessage = "Descripcion Requerida")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Descripcion detallada requerida")]
        public string LargeDescription { get; set; }

        [Required(ErrorMessage = "Precio Requerido")]
        public decimal Price { get; set; }

        public int? Stock { get; set; }

        public int? Status { get; set; }

        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Marca Requerida")]
        public int BrandId { get; set; }

        public string? NameBrand { get; set; }


        [Required(ErrorMessage = "Categoria Requerida")]
        public int CategoryId { get; set; }

        public string? NameCategory { get; set; }


        [Required(ErrorMessage = "Subcategoria Requerida")]
        public int SubCategoryId { get; set; }

        public string? NameSubcategory { get; set; }

        public List<IFormFile>? Images { get; set; }

    }
}
