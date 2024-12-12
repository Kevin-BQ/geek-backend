using AutoMapper;
using BLL.Services.Interfaces;
using Data;
using Data.Interfaces.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entities;
using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public OrderService(IWorkUnit workUnit, IMapper mapper,
            IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _workUnit = workUnit;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<OrderDto> AddOrder(OrderDto orderDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    throw new InvalidOperationException("Usuario no autenticado.");
                }

                int userId = int.Parse(userIdClaim.Value);

                var shoppingCart = await _workUnit.ShoppingCart.GetFirst(e => e.UserId == userId);
                if (shoppingCart == null)
                {
                    throw new InvalidOperationException("Carrito no encontrado");
                }

                var listaShopingCartItem = await _workUnit.ShoppingCarItem.GetAll(filtro: e => e.CartId == shoppingCart.Id);

                Order order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Now.AddDays(7),
                    Status = orderDto.Status == 1 ? true : false,
                    OrderStatus = Enum.TryParse<OrderStatus>(orderDto.OrderStatus, true, out var status)
                    ? status
                    : OrderStatus.Comprado,
                    Total =  0
                };

                await _workUnit.Order.Add(order);
                await _workUnit.Save();

                if (order.Id == 0)
                {
                    throw new TaskCanceledException("Error al crear la orden");
                }


                foreach (var item in listaShopingCartItem)
                {

                    var product = await _workUnit.Product.GetFirst(p => p.Id == item.ProductId);
                    if (product == null)
                    {
                        throw new InvalidOperationException("Producto no encontrado.");
                    }

                    if (product.Stock < item.Quantity)
                    {
                        throw new InvalidOperationException($"Stock insuficiente para {product.NameProduct}.");
                    }

                    product.Stock -= item.Quantity;
                    _workUnit.Product.Update(product);

                    decimal itemTotal = item.Price * item.Quantity - ((decimal)item.Product.Discount * item.Quantity);
                    order.Total += itemTotal;
                    

                    var OrderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        ListPrice= item.Price,
                        Discount = (decimal)item.Product.Discount,
                    };
                    await _workUnit.OrderItem.Add(OrderItem);

                    _workUnit.ShoppingCarItem.Remove(item);
                }

                _workUnit.Order.Update(order);

                await _workUnit.Save();

                await transaction.CommitAsync();

                return _mapper.Map<OrderDto>(order);

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task Remove(int id)
        {
            try
            {
                var orderDb = await _workUnit.Order.GetFirst(e => e.Id == id);
                if (orderDb == null)
                {
                    throw new TaskCanceledException("La orden no Existe");
                }

                orderDb.Status = !orderDb.Status;

                _workUnit.Order.Update(orderDb);
                await _workUnit.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    throw new InvalidOperationException("Usuario no autenticado.");
                }

                int userId = int.Parse(userIdClaim.Value);

                var lista = await _workUnit.Order.GetAll(
                                    filtro: e => e.UserId == userId,
                                    incluirPropiedades: "User",
                                    orderBy: e => e.OrderBy(e => e.Id));

                return _mapper.Map<IEnumerable<OrderDto>>(lista.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateStatusOrder(OrderDto orderDto)
        {
            try
            {
                var orderDb = await _workUnit.Order.GetFirst(e => e.Id == orderDto.Id);
                if (orderDb == null)
                {
                    throw new TaskCanceledException("La orden no Existe");
                }

                if (Enum.TryParse<OrderStatus>(orderDto.OrderStatus, true, out var status))
                {
                    orderDb.OrderStatus = status;
                }

                _workUnit.Order.Update(orderDb);
                await _workUnit.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
