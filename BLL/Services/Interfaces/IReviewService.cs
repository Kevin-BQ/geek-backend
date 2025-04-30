using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTOs;

namespace BLL.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetAllReviews();
        Task<ReviewDto> AddReview(ReviewDto reviewDto);
        Task UpdateReview(ReviewDto reviewDto);
        Task DeleteReview(int id);
    }
}
