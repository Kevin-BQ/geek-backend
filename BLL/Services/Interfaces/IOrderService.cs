using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrders();
        Task<OrderDto> AddOrder(OrderDto orderDto);
        Task UpdateStatusOrder(OrderDto orderDto);
        Task Remove(int id);
    }
}
