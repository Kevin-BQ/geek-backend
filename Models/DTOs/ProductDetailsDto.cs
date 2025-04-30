using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;

namespace Models.DTOs
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        public string NameProduct { get; set; }
        public string Description { get; set; }
        public string LargeDescription { get; set; }
        public decimal Price { get; set; }
        public int? Stock { get; set; }
        public bool Status { get; set; }
        public int BrandId { get; set; }
        public string NameBrand { get; set; }
        public int CategoryId { get; set; }
        public string NameCategory { get; set; }
        public int SubCategoryId { get; set; }
        public string NameSubCategory { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Review { get; set; }
        public List<string> Images { get; set; }
        public List<CommentResponseDto> Comments { get; set; } = new List<CommentResponseDto>();

    }

}
