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
    public class OrderService : IOrderService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;

        public OrderService(IWorkUnit workUnit, IMapper mapper)
        {
            _workUnit = workUnit;
            _mapper = mapper;
        }

        public async Task<OrderDto> AddOrder(OrderDto orderDto)
        {
            try
            {
                Order order = new Order
                {
                    UserId = orderDto.UserId,
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Now.AddDays(7), // 7 dias
                    Status = orderDto.Status == 1 ? true: false,
                };

                await _workUnit.Order.Add(order);
                await _workUnit.Save();

                if (order.Id == 0)
                {
                    throw new TaskCanceledException("Error al crear la orden");
                }

                return _mapper.Map<OrderDto>(order);

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
                var lista = await _workUnit.Order.GetAll(
                                    incluirPropiedades: "User",
                                    orderBy: e => e.OrderBy(e => e.Id));

                return _mapper.Map<IEnumerable<OrderDto>>(lista.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
