using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Comment comment)
        {
            var commentDb = _context.Comments.FirstOrDefault(e => e.Id == comment.Id);
            if (commentDb != null)
            {
                commentDb.Content = comment.Content;
                commentDb.Status = comment.Status;
                commentDb.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
