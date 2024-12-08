using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;

        public OrderItemService(IWorkUnit workUnit, IMapper mapper)
        {
            _workUnit = workUnit;
            _mapper = mapper;
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

        public async Task<IEnumerable<OrderItemDto>> GetAllOrderItems()
        {
            try
            {
                var lista = await _workUnit.OrderItem.GetAll(
                                    incluirPropiedades: "Order,Product",
                                    orderBy: e => e.OrderBy(e => e.Id));

                return _mapper.Map<IEnumerable<OrderItemDto>>(lista.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
