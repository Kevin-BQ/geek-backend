using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UrlImage { get; set; }

        [Required(ErrorMessage = "Producto Requerida")]
        public int ProductId { get; set; }

        [JsonIgnore]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
