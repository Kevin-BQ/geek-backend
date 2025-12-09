using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entidades;

namespace Models.Entities
{
    public class Comment
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

        [Required(ErrorMessage = "Contenido requerido")]
        [StringLength(1000, ErrorMessage = "Contenido demasiado largo")]
        public string Content { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int? ParentCommentId { get; set; }

        [ForeignKey("ParentCommentId")]
        public Comment ParentComment { get; set; } // Relación recursiva

        public ICollection<Comment> CommentsChild { get; set; } = new List<Comment>();
    }
}
