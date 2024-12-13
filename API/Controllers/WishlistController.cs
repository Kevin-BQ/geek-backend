using Azure;
using BLL.Services.Interfaces;
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


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProducts(int userAplicationId)
        {
            try
            {
                _response.Result = await _wishlistService.GetProducts(userAplicationId);
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

        


    }
}
