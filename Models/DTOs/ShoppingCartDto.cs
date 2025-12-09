using Models.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string NameUser { get; set; }

        public decimal? Discount { get; set; }
    }
}
