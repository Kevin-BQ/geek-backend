using Stripe.Checkout;

namespace BLL.Services.Interfaces
{
    public interface IStripeService
    {
        Task<string> CreateStripeSession(int orderId);
        Task<Session> GetSession(string sessionId);
    }
}
