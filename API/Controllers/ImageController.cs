using BLL.Services;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;

namespace API.Controllers
{
    public class ImageController: BaseApiController
    {
        private readonly IImageService _imageService;
        private ApiResponse _response;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
            _response = new();
        }

        [Authorize(Policy = "AdminRol")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Result = await _imageService.GetAllImages();
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

        [Authorize(Policy = "AdminRol")]
        [HttpPost("{idProduct:int}")]
        public async Task<IActionResult> Create( int idProduct, IFormFile imageFile)
        {
            try
            {
                await _imageService.AddImage(idProduct, imageFile);
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

        [Authorize(Policy = "AdminRol")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _imageService.Remove(id);
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
