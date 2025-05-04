using BLL.Services.Interfaces;
using Stripe.Checkout;

namespace API;

public class StripeService
{
    public static async Task<string> CreateStripeSession(int orderId, IShoppingCartItemService shoppingCartItemService)
    {
        var items = await shoppingCartItemService.GetAllShoppingItemCarts();
        
        if (items == null || !items.Any())
        {
            throw new TaskCanceledException("No se encontraron items");
        }
        
        // TODO: CAMBIAR A VARIABLES DE ENTORNO QUE ALMACENE LA URL DEL FRONTEND
        var frontendUrl = "http://localhost:4200";
        
        try
        {
            var options = new SessionCreateOptions()
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = items.Select(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "pen",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.NameProduct,
                            Description = item.Product.Description,
                            Images = item.Product.Images.Select(i => i.UrlImage).ToList() ?? []
                        },
                        UnitAmount = (long)(((1 - item.Product.Discount) * item.Price) * 100) // Convert to cents
                    },
                    Quantity = item.Quantity,
                }).ToList(),
                Mode = "payment",
                SuccessUrl = $"{frontendUrl}/checkout/success?orderId={orderId}",
                CancelUrl = $"{frontendUrl}/checkout/cancel?orderId={orderId}"
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Id;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    
    public static async Task<Session> GetSession(string sessionId)
    {
        try
        {
            var service = new SessionService();
            var session = await service.GetAsync(sessionId);
            
            if (session == null)
            {
                throw new TaskCanceledException("Session no encontrada");
            }

            return session;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}