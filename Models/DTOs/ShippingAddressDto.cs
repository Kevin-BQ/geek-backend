using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entidades;
using Models.Entities;

namespace Models.DTOs
{
    public class ShippingAddressDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string UserName { get; set; }

    }
}
