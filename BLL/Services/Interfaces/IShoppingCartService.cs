using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<ShoppingCartDto>> GetAllShoppingCarts();
        Task<ShoppingCartDto> AddShoppingCart(ShoppingCartDto shoppingCartDto);
        Task Remove(int id);
    }
}
