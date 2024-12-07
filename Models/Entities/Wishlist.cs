using Models.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Wishlist
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Usuario requerido")]
        public int UserAplicationId { get; set; }

        [ForeignKey("UserId")]
        public UserAplication UserAplication { get; set; }

        [Required(ErrorMessage = "Producto requerido")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }


    }
}
