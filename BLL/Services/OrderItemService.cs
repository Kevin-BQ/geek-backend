using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepositorio;
using Microsoft.AspNetCore.Http;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderItemService(IWorkUnit workUnit, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _workUnit = workUnit;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OrderItemDto> AddOrderItem(OrderItemDto orderItemDto)
        {
            try
            {
                OrderItem orderItem = new OrderItem
                {
                    OrderId = orderItemDto.OrderId,
                    ProductId = orderItemDto.ProductId,
                    Quantity = orderItemDto.Quantity,
                    ListPrice = orderItemDto.ListPrice,
                    Discount = orderItemDto.Discount
                };

                await _workUnit.OrderItem.Add(orderItem);
                await _workUnit.Save();

                if (orderItem.Id == 0)
                {
                    throw new TaskCanceledException("Error al crear la ordenItem");
                }

                return _mapper.Map<OrderItemDto>(orderItem);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Remove(int id)
        {
            try
            {
                var orderItemDb = await _workUnit.OrderItem.GetFirst(e => e.Id == id);
                if (orderItemDb == null)
                {
                    throw new TaskCanceledException("El carrito no Existe");
                }
                _workUnit.OrderItem.Remove(orderItemDb);
                await _workUnit.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<OrderItemDto>> GetAllOrderItems(int orderId)
        {
            try
            {
                var lista = await _workUnit.OrderItem.GetAll(
                                    filtro: e => e.OrderId == orderId,
                                    incluirPropiedades: "Order,Product.Brand,Product.Category,Product.Subcategory",
                                    orderBy: e => e.OrderBy(e => e.Id));

                return _mapper.Map<IEnumerable<OrderItemDto>>(lista.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsUser(int orderId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    throw new InvalidOperationException("Usuario no autenticado.");
                }

                int userId = int.Parse(userIdClaim.Value);

                var order = await _workUnit.Order.GetFirst(c => c.UserId == userId && c.Id == orderId);

                if (order == null)
                {
                    throw new InvalidOperationException("Orden no encontrada.");
                }

                var lista = await _workUnit.OrderItem.GetAll(
                    filtro: e => e.OrderId == order.Id,
                    incluirPropiedades: "Order,Product.Images,Product.Brand,Product.Category,Product.Subcategory");

                return lista.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
