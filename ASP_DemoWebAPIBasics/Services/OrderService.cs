using ASP_DemoWebAPIBasics.Models;
using ASP_DemoWebAPIBasics.Notifications;
using ASP_DemoWebAPIBasics.Payments;
using ASP_DemoWebAPIBasics.Repositories;

namespace ASP_DemoWebAPIBasics.Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        IPaymentProcessor _paymentProcessor;
        INotifier _notifier;

        public OrderService(IOrderRepository orderRepository, IPaymentProcessor paymentProcessor, INotifier notifier)
        {
            _orderRepository = orderRepository;
            _paymentProcessor = paymentProcessor;
            _notifier = notifier;
        }

        public void PlaceOrder(Order order)
        {
            _paymentProcessor.ProcessPayment(order.Amount);
            _orderRepository.Save(order);
            _notifier.Send("Your order has been placed successfully.");
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public Order? GetById(int id)
        {
            return _orderRepository.GetById(id);
        }

        public void Update(Order order)
        {
            _orderRepository.Update(order);
            _notifier.Send($"Your order {order.Id} has been updated.");
        }

        public void Delete(int id)
        {
            _orderRepository.Delete(id);
            _notifier.Send($"Your order {id} has been deleted.");
        }
    }
}
