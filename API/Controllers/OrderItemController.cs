using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;

namespace API.Controllers
{
    public class OrderItemController: BaseApiController
    {
        private readonly IOrderItemService _orderItemService;
        private ApiResponse _response;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
            _response = new();
        }

        [Authorize(Policy = "AllRol")]
        [HttpGet("{orderId:int}")]
        public async Task<IActionResult> Get(int orderId)
        {
            try
            {
                _response.Result = await _orderItemService.GetAllOrderItems(orderId);
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
        public async Task<IActionResult> Create(OrderItemDto orderItemDto)
        {
            try
            {
                await _orderItemService.AddOrderItem(orderItemDto);
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
                await _orderItemService.Remove(id);
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
