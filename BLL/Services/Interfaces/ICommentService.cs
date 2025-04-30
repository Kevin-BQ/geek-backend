using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTOs;
using Models.Entities;

namespace BLL.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetAllComments();
        Task<CommentDto> AddComment(CommentDto commentDto);
        Task UpdateComment(CommentDto commentDto);
        Task DeleteComment(int id);
    }
}
