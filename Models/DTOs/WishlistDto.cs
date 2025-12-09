using Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class WishlistDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Usuario requerido")]
        public int UserId { get; set; }

        public string nameUser { get; set; }

        [Required(ErrorMessage = "Producto requerido")]
        public int ProductId { get; set; }

        public string nameProduct { get; set; }


    }
}
