using AutoMapper;
using Floward.OnlineStore.ApplicationCore.Services.Interfaces;
using Floward.OnlineStore.Core.Models;
using Floward.OnlineStore.Core.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Threading.Tasks;

namespace Floward.OnlineStore.ApplicationCore.Services
{
    public class OrderService : ServiceBase, IOrderService
    {
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IConfiguration configuration)
            : base(unitOfWork, mapper, logger, configuration)
        {
        }

       public async Task AddToCartAsync(Order order)
       {
            order.OrderStatus = OrderStatus.InCart;
            var existingOrder = await _unitOfWork.OrderRepository.FirstOrDefaultAsync(c => c.OrderStatus == order.OrderStatus && c.ProductId == order.ProductId && c.UserId == order.UserId);

            if (existingOrder != null) 
                ++existingOrder.Quantity;
            else
                await _unitOfWork.OrderRepository.AddAsync(order);

            await _unitOfWork.CompleteAsync();
       }

        public async Task RemoveFromCartAsync(Order order)
        {
            var existingOrder = await _unitOfWork.OrderRepository.FirstOrDefaultAsync(c => c.OrderStatus == order.OrderStatus && c.ProductId == order.ProductId && c.UserId == order.UserId);

            if (existingOrder != null) return;

            if (existingOrder.Quantity > 1)
                --existingOrder.Quantity;
            else
                _unitOfWork.OrderRepository.Remove(existingOrder);

            await _unitOfWork.CompleteAsync();
        }

    }
}