using EF.Core.Repository.Interface.Repository;
using Ordering.Model.Models;

namespace Ordering.Applications.Contracts.Persistence.OrderRepositories
{
    public interface IOrderRepository : ICommonRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserNameAsync(string userName);
    }
}