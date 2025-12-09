using BLL.Services;
using BLL.Services.Interfaces;
using Models.Entities;
using Stripe.Checkout;

namespace API;

public class StripeService : IStripeService
{
    private readonly IOrderItemService _orderItemService; 

    public StripeService(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    public async Task<string> CreateStripeSession(int orderId)
    {
        var items = await _orderItemService.GetAllOrderItemsUser(orderId);

        if (items == null || !items.Any())
        {
            throw new InvalidOperationException("No se encontraron items en la orden");
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
                            Images = item.Product.Images.Select(i => i.UrlImage).ToList() ?? new List<string>()
                        },
                        UnitAmount = (long)(((1 - item.Product.Discount) * item.ListPrice) * 100) // Convert to cents
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

    public async Task<Session> GetSession(string sessionId)
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