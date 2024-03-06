using MicroserviceDemo.CatalogApi.Models;
using MongoRepo.Interfaces.Manager;

namespace MicroserviceDemo.CatalogApi.Interfaces.Manager
{
    public interface IProductManager : ICommonManager<Product>
    {
    }
}