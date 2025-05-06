using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Enum;

namespace Models.Entities
{
    public class Shipment
    {
        public int Id { get; set; }

        public DateTime ShipmentDate { get; set; }

        public ShipmentStatus StatusShipment { get; set; }
        public string ShipmentMethod { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
