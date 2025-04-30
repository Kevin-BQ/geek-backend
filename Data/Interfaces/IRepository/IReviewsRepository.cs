using Data.Interfaces.IRepositorio;
using Models.Entities;

namespace Data.Interfaces.IRepository
{
    public interface IReviewsRepository: IGenericRepository<Review>
    {
        void update(Review review);
    }
}
