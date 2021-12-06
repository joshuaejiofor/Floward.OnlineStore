using Floward.OnlineStore.Core.Requests;
using System.Threading.Tasks;

namespace Floward.OnlineStore.ApplicationCore.Providers.Interfaces
{
    public interface IProductProvider
    {
        Task CalculateDiscountAsync(PurchaseRequest purchaseRequest);
        Task AddToCartAsync(PurchaseRequest purchaseRequest);
    }
}
