using WholesaleRetailAPI.Models;
using static WholesaleRetailAPI.Controllers.OrdersController;

namespace WholesaleRetailAPI.Services
{
    public interface IPricingService
    {
        Task<QuoteResponse> GetQuoteAsync(CreateOrderRequest request);
    }
}
