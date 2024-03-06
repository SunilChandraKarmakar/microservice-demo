using MicroserviceDemo.CatalogApi.Interfaces.Manager;
using MicroserviceDemo.CatalogApi.Models;
using MicroserviceDemo.CatalogApi.Repository;
using MongoRepo.Manager;

namespace MicroserviceDemo.CatalogApi.Manager
{
    public class ProductManager : CommonManager<Product>, IProductManager
    {
        public ProductManager() : base(new ProductRepository())
        {
        }
    }
}