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
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;

        public ShoppingCartItemService(IWorkUnit workUnit, IMapper mapper)
        {
            _workUnit = workUnit;
            _mapper = mapper;
        }

        public async Task<ShoppingCartItemDto> AddShoppingCartItem(ShoppingCartItemDto shoppingCartItemDto)
        {
            try
            {

                ShoppingCartItem shoppingCartItem = new ShoppingCartItem
                {
                    CartId = shoppingCartItemDto.CartId,
                    ProductId = shoppingCartItemDto.ProductId,
                    Quantity = shoppingCartItemDto.Quantity,
                    Price = shoppingCartItemDto.Price,
                };

                await _workUnit.ShoppingCarItem.Add(shoppingCartItem);
                await _workUnit.Save();

                if (shoppingCartItem.Id == 0)
                {
                    throw new TaskCanceledException("No se pudo agregar el producto al carrito");
                }

                return _mapper.Map<ShoppingCartItemDto>(shoppingCartItem);
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
                var shopingCartItemDb = await _workUnit.ShoppingCarItem.GetFirst(e => e.Id == id);
                if (shopingCartItemDb == null)
                {
                    throw new TaskCanceledException("El carrito no Existe");
                }
                _workUnit.ShoppingCarItem.Remove(shopingCartItemDb);
                await _workUnit.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<ShoppingCartItemDto>> GetAllShoppingItemCarts()
        {
            try
            {
                var lista = await _workUnit.ShoppingCarItem.GetAll(
                                    incluirPropiedades: "ShoppingCart,Product",
                                    orderBy: e => e.OrderBy(e => e.Id));

                return _mapper.Map<IEnumerable<ShoppingCartItemDto>>(lista.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
