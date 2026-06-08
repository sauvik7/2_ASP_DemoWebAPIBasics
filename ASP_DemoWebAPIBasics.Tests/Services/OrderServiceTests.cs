using Xunit;
using Moq;
using ASP_DemoWebAPIBasics.Repositories;
using ASP_DemoWebAPIBasics.Payments;
using ASP_DemoWebAPIBasics.Notifications;
using ASP_DemoWebAPIBasics.Services;
using ASP_DemoWebAPIBasics.Models;
using System.Collections.Generic;

namespace ASP_DemoWebAPIBasics.Tests.Services
{
    public class OrderServiceTests
    {
        [Fact]
        public void PlaceOrder_ProcessesPaymentAndSavesAndNotifies()
        {
            var repo = new Mock<IOrderRepository>();
            var payment = new Mock<IPaymentProcessor>();
            var notifier = new Mock<INotifier>();

            var svc = new OrderService(repo.Object, payment.Object, notifier.Object);

            var order = new Order { Amount = 10 };

            svc.PlaceOrder(order);

            payment.Verify(p => p.ProcessPayment(10), Times.Once);
            repo.Verify(r => r.Save(order), Times.Once);
            notifier.Verify(n => n.Send(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetAll_DelegatesToRepository()
        {
            var repo = new Mock<IOrderRepository>();
            repo.Setup(r => r.GetAll()).Returns(new List<Order> { new Order { Amount = 1 } });
            var payment = new Mock<IPaymentProcessor>();
            var notifier = new Mock<INotifier>();

            var svc = new OrderService(repo.Object, payment.Object, notifier.Object);

            var all = svc.GetAll();

            Assert.Single(all);
            repo.Verify(r => r.GetAll(), Times.Once);
        }

        [Fact]
        public void Update_CallsRepoAndNotifies()
        {
            var repo = new Mock<IOrderRepository>();
            var payment = new Mock<IPaymentProcessor>();
            var notifier = new Mock<INotifier>();

            var svc = new OrderService(repo.Object, payment.Object, notifier.Object);
            var order = new Order { Id = 1, Amount = 5 };

            svc.Update(order);

            repo.Verify(r => r.Update(order), Times.Once);
            notifier.Verify(n => n.Send(It.Is<string>(s => s.Contains("updated"))), Times.Once);
        }
    }
}
