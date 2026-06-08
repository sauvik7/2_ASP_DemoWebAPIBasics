using ASP_DemoWebAPIBasics.Models;
using ASP_DemoWebAPIBasics.DAL;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ASP_DemoWebAPIBasics.Repositories
{
    public class SqlOrderRepository : IOrderRepository
    {
        private readonly DemoWebAPIBasicsDbContext _dbContext;

        public SqlOrderRepository(DemoWebAPIBasicsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            Console.WriteLine($"Order saved successfully: {order.Id}");
        }

        public IEnumerable<Order> GetAll()
        {
            return _dbContext.Orders.AsNoTracking().ToList();
        }

        public Order? GetById(int id)
        {
            return _dbContext.Orders.Find(id);
        }

        public void Update(Order order)
        {
            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();
            Console.WriteLine($"Order updated successfully: {order.Id}");
        }

        public void Delete(int id)
        {
            var order = _dbContext.Orders.Find(id);
            if (order is null) return;
            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();
            Console.WriteLine($"Order deleted successfully: {id}");
        }
    }
}
