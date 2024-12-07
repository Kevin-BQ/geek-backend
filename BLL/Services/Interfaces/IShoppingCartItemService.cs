﻿using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IShoppingCartItemService
    {
        Task<IEnumerable<ShoppingCartItemDto>> GetAllShoppingItemCarts();
        Task<ShoppingCartItemDto> AddShoppingCartItem(ShoppingCartItemDto shoppingCartItemDto);
        Task Remove(int id);
    }
}
