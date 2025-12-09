using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Models.Entidades;

namespace Models.Entities
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Producto requerido")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserAplication User { get; set; }

        [Required(ErrorMessage = "Reseña requerida")]
        [Range(1, 5, ErrorMessage = "Reseña entre 1 y 5")]
        public byte Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Máximo 1000 caracteres")]
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
