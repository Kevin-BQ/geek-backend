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
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public UserAplication User { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public bool Status { get; set; }

    }
}
