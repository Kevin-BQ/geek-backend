using System.Security.Claims;
using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepositorio;
using Microsoft.AspNetCore.Http;
using Models.DTOs;
using Models.Entities;

namespace BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReviewService(IWorkUnit workUnit, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _workUnit = workUnit;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ReviewDto> AddReview(ReviewDto reviewDto)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    throw new InvalidOperationException("Usuario no autenticado.");
                }

                int userId = int.Parse(userIdClaim.Value);

                Review review = new Review
                {
                    ProductId = reviewDto.ProductId,
                    UserId = userId,
                    Comment = reviewDto.Comment,
                    Rating = reviewDto.Rating
                    
                };

                await _workUnit.Review.Add(review);
                await _workUnit.Save();

                if (review.Id == 0)
                {
                    throw new TaskCanceledException("No se puede realizar esta acción");
                }

                return _mapper.Map<ReviewDto>(review);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateReview(ReviewDto reviewDto)
        {
            try
            {
                var reviewDb = await _workUnit.Review.GetFirst(e => e.Id == reviewDto.Id);

                if (reviewDb == null)
                {
                    throw new TaskCanceledException("La Reseña no Existe");
                }

                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    throw new InvalidOperationException("Usuario no autenticado.");
                }

                if (!int.TryParse(userIdClaim.Value, out var userId))
                {
                    throw new InvalidOperationException("ID de usuario inválido.");
                }

                var isAdmin = _httpContextAccessor.HttpContext.User.IsInRole("Admin");

                if (reviewDb.UserId != userId && !isAdmin)
                {
                    throw new TaskCanceledException("No puede realizar esta acción");
                }

                reviewDb.Comment = reviewDto.Comment;
                reviewDb.Rating = reviewDto.Rating;
                

                _workUnit.Review.update(reviewDb);

                await _workUnit.Save();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteReview(int id)
        {
            try
            {
                var reviewDb = await _workUnit.Review.GetFirst(e => e.Id == id);

                if (reviewDb == null)
                {
                    throw new TaskCanceledException("La Reseña no Existe");
                }

                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    throw new InvalidOperationException("Usuario no autenticado.");
                }

                if (!int.TryParse(userIdClaim.Value, out var userId))
                {
                    throw new InvalidOperationException("ID de usuario inválido.");
                }

                var isAdmin = _httpContextAccessor.HttpContext.User.IsInRole("Admin");

                if (reviewDb.UserId != userId && !isAdmin)
                {
                    throw new TaskCanceledException("No puede realizar esta acción");
                }

                _workUnit.Review.Remove(reviewDb);
                await _workUnit.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ReviewDto>> GetAllReviews()
        {
            try
            {
                var lista = await _workUnit.Review.GetAll(
                                    incluirPropiedades: "User,Product.Images,Product.Brand,Product.Category,Product.Subcategory",
                                    orderBy: e => e.OrderBy(e => e.Id));

                return _mapper.Map<IEnumerable<ReviewDto>>(lista.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
