using Data.Interfaces.IRepository;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ShoppingCartItemsRepository: Repository<ShoppingCartItem>, IShoppingCarItemsRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartItemsRepository(ApplicationDbContext context): base(context) 
        {        
            _context = context;
        }
    }
}
