using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;
using Models.Enum;

namespace Models.DTOs
{
    public class ShipmentDto
    {
        public int Id { get; set; }

        public DateTime ShipmentDate { get; set; }

        public string StatusShipment { get; set; }
        public string ShipmentMethod { get; set; }

        public int OrderId { get; set; }

    }
}
