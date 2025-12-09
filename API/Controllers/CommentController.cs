using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;

namespace API.Controllers
{
    public class CommentController: BaseApiController
    {
        private readonly ICommentService _commentService;
        private ApiResponse _response;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
            _response = new();
        }

        [Authorize(Policy = "AdminRol")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Result = await _commentService.GetAllComments();
                _response.IsSuccessful = true;
                _response.statusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {

                _response.IsSuccessful = false;
                _response.Message = ex.Message;
                _response.statusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        [Authorize(Policy = "AllRol")]
        [HttpPost]
        public async Task<IActionResult> Create(CommentDto commentDto)
        {
            try
            {
                await _commentService.AddComment(commentDto);
                _response.IsSuccessful = true;
                _response.statusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {

                _response.IsSuccessful = false;
                _response.Message = ex.Message;
                _response.statusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        [Authorize(Policy = "AllRol")]
        [HttpPut]
        public async Task<IActionResult> Edit(CommentDto commentDto)
        {
            try
            {
                await _commentService.UpdateComment(commentDto);
                _response.IsSuccessful = true;
                _response.statusCode = HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {

                _response.IsSuccessful = false;
                _response.Message = ex.Message;
                _response.statusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        [Authorize(Policy = "AllRol")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _commentService.DeleteComment(id);
                _response.IsSuccessful = true;
                _response.statusCode = HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {

                _response.IsSuccessful = false;
                _response.Message = ex.Message;
                _response.statusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }
    }
}
