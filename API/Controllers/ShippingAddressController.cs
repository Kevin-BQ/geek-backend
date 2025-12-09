using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;

namespace API.Controllers
{
    public class ShippingAddressController: BaseApiController
    {
        private readonly IShippingAddressService _shippingAddressService;
        private ApiResponse _response;

        public ShippingAddressController(IShippingAddressService shippingAddressService)
        {
            _shippingAddressService = shippingAddressService;
            _response = new();
        }

        [Authorize(Policy = "AdminRol")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Result = await _shippingAddressService.GetAllShippingAddresses();
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

        [HttpGet("Active")]
        public async Task<IActionResult> GetAllForUser()
        {
            try
            {
                _response.Result = await _shippingAddressService.GetShippingAddressActive();
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

        [HttpPost]
        public async Task<IActionResult> Create(ShippingAddressDto shippingAddressDto)
        {
            try
            {
                await _shippingAddressService.AddShippingAddress(shippingAddressDto);
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

        [HttpPut]
        public async Task<IActionResult> Edit(ShippingAddressDto shippingAddressDto)
        {
            try
            {
                await _shippingAddressService.UpdateShippingAddress(shippingAddressDto);
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

        [HttpPut("{shippingAddressId:int}")]
        public async Task<IActionResult> UpdateStatus(int shippingAddressId)
        {
            try
            {
                await _shippingAddressService.UpdateStatus(shippingAddressId);
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
