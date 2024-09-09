using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Ordering.Applications.Contracts.Persistence.OrderRepositories;
using Ordering.Infrastructure.Persistence;
using Ordering.Model.Models;

namespace Ordering.Infrastructure.Repositories.OrderRepository
{
    public class OrderRepository : CommonRepository<Order>, IOrderRepository
    {
        private readonly OrdringDbContext _context;

        public OrderRepository(OrdringDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserNameAsync(string userName)
        {
            var getOrders = await _context.Orders
                .Where(o => o.UserName.ToLower() ==  userName.ToLower())
                .ToListAsync();

            return getOrders;
        }
    }
}