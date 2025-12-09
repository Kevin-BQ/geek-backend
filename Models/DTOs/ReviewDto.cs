

using Models.Entidades;
using Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? NameProduct { get; set; }
        public int UserId { get; set; }
        public string? NameUser { get; set; }
        public byte Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
