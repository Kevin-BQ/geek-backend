using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entidades;
using Models.Entities;

namespace Models.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? NameProduct { get; set; }
        public int UserId { get; set; }
        public string? NameUser { get; set; }
        public string? Content { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
