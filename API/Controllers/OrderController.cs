using BLL.Services;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;

namespace API.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IShoppingCartItemService _shoppingCartItemService;
        private ApiResponse _response;

        public OrderController(IOrderService orderService, IShoppingCartItemService shoppingCartItemService)
        {
            _orderService = orderService;
            _shoppingCartItemService = shoppingCartItemService;
            _response = new();
        }

        [Authorize(Policy = "AllRol")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Result = await _orderService.GetAllOrders();
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
        public async Task<IActionResult> Create(OrderDto orderDto)
        {
            try
            {
                var sessionId = await StripeService.CreateStripeSession(orderDto.Id, _shoppingCartItemService);
                var order = await _orderService.AddOrder(orderDto);

                _response.Result = new { sessionId };
                _response.IsSuccessful = true;
                _response.statusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.Message = ex.Message;
                _response.statusCode = HttpStatusCode.BadRequest;

                return BadRequest(_response);
            }

            return Ok(_response);
        }

        [Authorize(Policy = "AllRol")]
        [HttpPost("success")]
        public async Task<IActionResult> Success(int orderId)
        {
            try
            {
                // TODO: GET ORDER WITH ORDER ID
                
                // TODO: get session with session id of order
                // var session = await StripeService.GetSession(orderId);
                
                // if (session.Status == "succeeded")
                // {
                //     // TODO: UPDATE ORDER STATUS
                //     
                //     return Ok(_response);
                // }
                // else
                // {
                //     // Mensaje de error
                // }

                _response.Result = 
                _response.IsSuccessful = true;
                _response.statusCode = HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.Message = ex.Message;
                _response.statusCode = HttpStatusCode.BadRequest;

                return BadRequest(_response);
            }

            return Ok(_response);
        }

        [Authorize(Policy = "AdminRol")]
        [HttpPut]
        public async Task<IActionResult> Edit(OrderDto orderDto)
        {
            try
            {
                await _orderService.UpdateStatusOrder(orderDto);
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
        [HttpPost("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _orderService.Remove(id);
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