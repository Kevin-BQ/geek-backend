using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepositorio;
using Microsoft.AspNetCore.Http;
using Models.DTOs;
using Models.Entities;

namespace BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentService(IWorkUnit workUnit, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _workUnit = workUnit;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<CommentDto> AddComment(CommentDto commentDto)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    throw new InvalidOperationException("Usuario no autenticado.");
                }

                int userId = int.Parse(userIdClaim.Value);

                if (commentDto.ParentCommentId.HasValue && commentDto.ParentCommentId > 0)
                {
                    var parentExists = await _workUnit.Comment.GetFirst(c => c.Id == commentDto.ParentCommentId.Value) != null;

                    if (!parentExists)
                    {
                        throw new ArgumentException("El comentario padre especificado no existe.");
                    }
                }

                Comment review = new Comment
                {
                    ProductId = commentDto.ProductId,
                    UserId = userId,
                    Content = commentDto.Content,
                    Status = commentDto.Status == 1 ? true : false,
                    ParentCommentId = commentDto.ParentCommentId == 0 ? null : commentDto.ParentCommentId
                };

                await _workUnit.Comment.Add(review);
                await _workUnit.Save();

                if (review.Id == 0)
                {
                    throw new TaskCanceledException("No se puede realizar esta acción");
                }

                return _mapper.Map<CommentDto>(review);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateComment(CommentDto commentDto)
        {
            try
            {
                var commentDb = await _workUnit.Comment.GetFirst(e => e.Id == commentDto.Id);
                if (commentDb == null)
                {
                    throw new TaskCanceledException("El comentario no Existe");
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

                if (commentDb.UserId != userId && !isAdmin)
                {
                    throw new TaskCanceledException("No puede realizar esta acción");
                }

                commentDb.Content = commentDto.Content;
                commentDb.Status = commentDto.Status == 1 ? true : false;

                _workUnit.Comment.Update(commentDb);
                await _workUnit.Save();

            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task DeleteComment(int id)
        {
            try
            {
                var commentDb = await _workUnit.Comment.GetFirst(e => e.Id == id);

                if (commentDb == null)
                {
                    throw new TaskCanceledException("El comentario no Existe");
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

                if (commentDb.UserId != userId && !isAdmin)
                {
                    throw new TaskCanceledException("No puede realizar esta acción");
                }

                commentDb.Status = !commentDb.Status;

                _workUnit.Comment.Update(commentDb);
                await _workUnit.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<CommentDto>> GetAllComments()
        {
            try
            {
                var lista = await _workUnit.Comment.GetAll(
                    incluirPropiedades: "User,Product.Images,Product.Brand,Product.Category,Product.Subcategory",
                    orderBy: e => e.OrderBy(e => e.Id));

                return _mapper.Map<IEnumerable<CommentDto>>(lista.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
