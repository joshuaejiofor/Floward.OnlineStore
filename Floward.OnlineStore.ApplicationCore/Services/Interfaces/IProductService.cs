using Floward.OnlineStore.Core.Requests;
using System.Threading.Tasks;

namespace Floward.OnlineStore.ApplicationCore.Services.Interfaces
{
    public interface IProductService
    {
        Task ProcessAsync(PurchaseRequest purchaseRequest);
    }
}
