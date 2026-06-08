using ASP_DemoWebAPIBasics.Models;

namespace ASP_DemoWebAPIBasics.Repositories
{
    public interface IOrderRepository
    {
        void Save(Order order);
        IEnumerable<Order> GetAll();
        Order? GetById(int id);
        void Update(Order order);
        void Delete(int id);
    }
}
