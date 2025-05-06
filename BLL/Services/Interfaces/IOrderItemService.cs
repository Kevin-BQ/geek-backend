using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDto>> GetAllOrderItems(int orderId);
        Task<IEnumerable<OrderItem>> GetAllOrderItemsUser(int orderId);
        Task<OrderItemDto> AddOrderItem(OrderItemDto orderItemDto);
        Task Remove(int id);
    }
}
