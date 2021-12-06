using AutoMapper;
using Floward.OnlineStore.ApplicationCore.Services;
using Floward.OnlineStore.Core.Models;
using Floward.OnlineStore.Core.UnitOfWork;
using Floward.OnlineStore.WebApi.Tests.Extensions;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Floward.OnlineStore.WebApi.Tests.UnitTests
{
    public class UnitTestBase
    {
        protected readonly OrderService _orderService;
        public List<Order> Orders { get; set; }
        protected readonly Order _order;
        protected readonly Order _newOrder;

        public UnitTestBase()
        {
            //Arrange for all tests..
            var unitOfWork = new Mock<IUnitOfWork>();
            var mapper = new Mock<IMapper>();
            var configuration = new Mock<IConfiguration>();
            var logger = new Mock<ILogger>();

            _orderService = new OrderService(unitOfWork.Object, mapper.Object, logger.Object, configuration.Object);
            Orders = JsonConvert.DeserializeObject<List<Order>>(File.ReadAllText(Path.Combine(@"TestData.json")));
            _order = Orders.ToArray()[0];

            _newOrder = new Order { Id = 10, ProductId = 2, Quantity = 2, OrderStatus = OrderStatus.InCart, UserId = "3ae406ea-aece-41ba-999a-0f86434a3201" }; Orders.Add(_newOrder);

            unitOfWork.Setup(c => c.OrderRepository.GetAll()).Returns(Orders.GetQueryableMockDbSet());
            unitOfWork.Setup(c => c.OrderRepository.Find(It.IsAny<Expression<Func<Order, bool>>>())).Returns(Orders.AsQueryable());
            unitOfWork.Setup(c => c.OrderRepository.AddAsync(It.IsAny<Order>()))
                .Callback((Order order) => Orders.Add(_newOrder));
            unitOfWork.Setup(c => c.OrderRepository.Remove(It.IsAny<Order>()))
                .Callback((Order order) => Orders.Remove(_order));
            unitOfWork.Setup(c => c.OrderRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Order, bool>>>())).Returns(Task.FromResult(_order));
            unitOfWork.Setup(c => c.OrderRepository.SingleOrDefaultAsync(It.IsAny<Expression<Func<Order, bool>>>())).Returns(Task.FromResult<Order>(null));
        }
    }
}
