using System.Threading.Tasks;
using Xunit;

namespace Floward.OnlineStore.WebApi.Tests.UnitTests
{
    public class OrderServiceTests : UnitTestBase
    {
        [Fact]
        public async Task AddToCartAsync_Test()
        {
            await _orderService.AddToCartAsync(_newOrder);
            Assert.Contains(_newOrder, Orders);
        }

        [Fact]
        public async Task RemoveFromCartAsync_Test()
        {
            await _orderService.RemoveFromCartAsync(_order);
            Assert.Contains(_order, Orders);
            Assert.Equal(2, Orders.ToArray()[0].Quantity);
        }
    }
}
