using Xunit;
using Moq;
using ASP_DemoWebAPIBasics.Services;
using ASP_DemoWebAPIBasics.Controllers;
using ASP_DemoWebAPIBasics.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ASP_DemoWebAPIBasics.Tests.Controllers
{
    public class OrdersApiControllerTests
    {
        [Fact]
        public void PlaceOrder_ReturnsOk_WhenValid()
        {
            var svc = new Mock<IOrderService>();
            var ctrl = new OrdersApiController(svc.Object);

            var order = new Order { Amount = 5 };
            var result = ctrl.PlaceOrder(order);

            Assert.IsType<OkObjectResult>(result);
            svc.Verify(s => s.PlaceOrder(order), Times.Once);
        }

        [Fact]
        public void GetAll_ReturnsOkWithOrders()
        {
            var svc = new Mock<IOrderService>();
            svc.Setup(s => s.GetAll()).Returns(new List<Order> { new Order { Amount = 1 } });
            var ctrl = new OrdersApiController(svc.Object);

            var result = ctrl.GetAll();

            var ok = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<Order>>(ok.Value);
            Assert.Single(list);
        }

        [Fact]
        public void GetById_ReturnsNotFound_WhenMissing()
        {
            var svc = new Mock<IOrderService>();
            svc.Setup(s => s.GetById(1)).Returns((Order?)null);
            var ctrl = new OrdersApiController(svc.Object);

            var result = ctrl.GetById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Update_ReturnsNoContent_OnSuccess()
        {
            var svc = new Mock<IOrderService>();
            svc.Setup(s => s.GetById(1)).Returns(new Order { Id = 1, Amount = 2 });
            var ctrl = new OrdersApiController(svc.Object);

            var result = ctrl.Update(1, new Order { Id = 1, Amount = 3 });

            Assert.IsType<NoContentResult>(result);
            svc.Verify(s => s.Update(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void Delete_ReturnsNoContent_OnSuccess()
        {
            var svc = new Mock<IOrderService>();
            svc.Setup(s => s.GetById(1)).Returns(new Order { Id = 1, Amount = 2 });
            var ctrl = new OrdersApiController(svc.Object);

            var result = ctrl.Delete(1);

            Assert.IsType<NoContentResult>(result);
            svc.Verify(s => s.Delete(1), Times.Once);
        }
    }
}
