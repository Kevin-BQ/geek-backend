using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Repository
{
    public class ReviewRepository : Repository<Review>, IReviewsRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void update(Review review)
        {
            var reviewDb = _context.Reviews.FirstOrDefault(e => e.Id == review.Id);

            if (review != null)
            {
                reviewDb.Rating = review.Rating;
                reviewDb.Comment = review.Comment;  
                reviewDb.UpdatedAt = DateTime.Now;

                _context.SaveChanges();
            }
        }
    }
}
