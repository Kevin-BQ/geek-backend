using Data.Interfaces.IRepository;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class OrderRepository: Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context): base(context) 
        {
            _context = context;
        }

        public void Update(Order order)
        {
            var orderDb = _context.Orders.FirstOrDefault(e => e.Id == order.Id);

            if (order != null)
            {
                orderDb.Status = order.Status;
                orderDb.OrderStatus = order.OrderStatus;
                orderDb.SessionId = order.SessionId;
                _context.SaveChanges();
            }
        }
    }
}
