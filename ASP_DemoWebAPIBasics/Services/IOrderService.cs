using ASP_DemoWebAPIBasics.Models;
using System.Collections.Generic;

namespace ASP_DemoWebAPIBasics.Services
{
    public interface IOrderService
    {
        void PlaceOrder(Order order);
        IEnumerable<Order> GetAll();
        Order? GetById(int id);
        void Update(Order order);
        void Delete(int id);
    }
}
