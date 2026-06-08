using Xunit;
using ASP_DemoWebAPIBasics.Repositories;
using ASP_DemoWebAPIBasics.DAL;
using ASP_DemoWebAPIBasics.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ASP_DemoWebAPIBasics.Tests.Repositories
{
    public class SqlOrderRepositoryTests
    {
        private DemoWebAPIBasicsDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DemoWebAPIBasicsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new DemoWebAPIBasicsDbContext(options);
        }

        [Fact]
        public void Save_AddsOrder()
        {
            using var ctx = CreateInMemoryContext();
            var repo = new SqlOrderRepository(ctx);

            var order = new Order { Amount = 12.34m };
            repo.Save(order);

            Assert.Equal(1, ctx.Orders.Count());
            Assert.Equal(12.34m, ctx.Orders.First().Amount);
        }

        [Fact]
        public void GetAll_ReturnsAll()
        {
            using var ctx = CreateInMemoryContext();
            ctx.Orders.Add(new Order { Amount = 1 });
            ctx.Orders.Add(new Order { Amount = 2 });
            ctx.SaveChanges();

            var repo = new SqlOrderRepository(ctx);
            var all = repo.GetAll().ToList();

            Assert.Equal(2, all.Count);
        }

        [Fact]
        public void GetById_ReturnsOrder()
        {
            using var ctx = CreateInMemoryContext();
            var o = new Order { Amount = 5 };
            ctx.Orders.Add(o);
            ctx.SaveChanges();

            var repo = new SqlOrderRepository(ctx);
            var found = repo.GetById(o.Id);

            Assert.NotNull(found);
            Assert.Equal(5, found.Amount);
        }

        [Fact]
        public void Update_UpdatesOrder()
        {
            using var ctx = CreateInMemoryContext();
            var o = new Order { Amount = 5 };
            ctx.Orders.Add(o);
            ctx.SaveChanges();

            var repo = new SqlOrderRepository(ctx);
            o.Amount = 9;
            repo.Update(o);

            var updated = ctx.Orders.Find(o.Id);
            Assert.Equal(9, updated.Amount);
        }

        [Fact]
        public void Delete_RemovesOrder()
        {
            using var ctx = CreateInMemoryContext();
            var o = new Order { Amount = 5 };
            ctx.Orders.Add(o);
            ctx.SaveChanges();

            var repo = new SqlOrderRepository(ctx);
            repo.Delete(o.Id);

            Assert.Null(ctx.Orders.Find(o.Id));
        }
    }
}
