using Models.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? NameUser { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public int Status { get; set; }
        public string? OrderStatus { get; set; }
        public decimal? Total { get; set; }
        public int ShippingAddressId { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? ShippingMethod { get; set; }
        public DateTime ShippingDate { get; set; }
        public string? SessionId { get; set; }
    }
}
