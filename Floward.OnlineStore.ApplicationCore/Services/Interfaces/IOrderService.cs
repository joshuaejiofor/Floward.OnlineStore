using Floward.OnlineStore.Core.Models;
using System.Threading.Tasks;

namespace Floward.OnlineStore.ApplicationCore.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddToCartAsync(Order order);
        Task RemoveFromCartAsync(Order order);
    }
}
