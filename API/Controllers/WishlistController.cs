using Azure;
using BLL.Services;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;

namespace API.Controllers
{
    public class WishlistController : BaseApiController
    {

        private readonly IWishlistService _wishlistService;
        private ApiResponse _response;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
            _response = new();
        }

        [Authorize(Policy = "AllRol")]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                _response.Result = await _wishlistService.GetMyWishlist();
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
        public async Task<IActionResult> Create(WishlistDto wishlistDto)
        {
            try
            {
                await _wishlistService.AddWishlist(wishlistDto);
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
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _wishlistService.DeleteWishlist(id);
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
