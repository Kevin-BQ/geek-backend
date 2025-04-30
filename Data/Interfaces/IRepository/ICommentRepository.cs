using Data.Interfaces.IRepositorio;
using Models.Entities;

namespace Data.Interfaces.IRepository
{
    public interface ICommentRepository: IGenericRepository<Comment>
    {
        void Update(Comment comment);
    }
}
