using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Models.DTOs;
using Models.Entidades;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCartItemService(IWorkUnit workUnit, IMapper mapper, 
            IHttpContextAccessor httpContextAccessor)
        {
            _workUnit = workUnit;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ShoppingCartItemDto> AddShoppingCartItem(ShoppingCartItemDto shoppingCartItemDto)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    throw new InvalidOperationException("Usuario no autenticado.");
                }

                int userId = int.Parse(userIdClaim.Value); 
                
                var existingCart = await _workUnit.ShoppingCart.GetFirst(c => c.UserId == userId);
                if (existingCart == null)
                {
                    ShoppingCart shoppingCart = new ShoppingCart
                    {
                        UserId = userId,
                        Discount = 0
                    };

                    await _workUnit.ShoppingCart.Add(shoppingCart);
                    await _workUnit.Save();
                
                }

                var producDb = await _workUnit.Product.GetFirst(c => c.Id == shoppingCartItemDto.ProductId);

                ShoppingCartItem shoppingCartItem = new ShoppingCartItem
                {
                    CartId = existingCart.Id,
                    ProductId = shoppingCartItemDto.ProductId,
                    Quantity = shoppingCartItemDto.Quantity,
                    Price = producDb.Price,
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
        public async Task<IEnumerable<ShoppingCartItem>> GetAllShoppingItemCarts()
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    throw new InvalidOperationException("Usuario no autenticado.");
                }

                int userId = int.Parse(userIdClaim.Value);

                var existingCart = await _workUnit.ShoppingCart.GetFirst(c => c.UserId == userId);

                if (existingCart == null)
                {
                    ShoppingCart shoppingCart = new ShoppingCart
                    {
                        UserId = userId,
                        Discount = 0
                    };

                    await _workUnit.ShoppingCart.Add(shoppingCart);
                    await _workUnit.Save();
                }

                var lista = await _workUnit.ShoppingCarItem.GetAll(
                    filtro: e => e.CartId == existingCart.Id,
                    incluirPropiedades: "ShoppingCart,Product.Images,Product.Brand,Product.Category,Product.Subcategory");

                return _mapper.Map<IEnumerable<ShoppingCartItem>>(lista.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
