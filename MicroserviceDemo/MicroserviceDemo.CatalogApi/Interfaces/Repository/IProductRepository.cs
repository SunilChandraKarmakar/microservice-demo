using MicroserviceDemo.CatalogApi.Models;
using MongoRepo.Interfaces.Repository;

namespace MicroserviceDemo.CatalogApi.Interfaces.Repository
{
    public interface IProductRepository : ICommonRepository<Product>
    {
    }
}