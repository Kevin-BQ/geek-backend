using System.ComponentModel.DataAnnotations.Schema;
using Models.Entidades;

namespace Models.Entities
{
    public class ShippingAddress
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public bool State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public UserAplication User { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
