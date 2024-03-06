using MicroserviceDemo.CatalogApi.Context;
using MicroserviceDemo.CatalogApi.Interfaces.Repository;
using MicroserviceDemo.CatalogApi.Models;
using MongoRepo.Repository;

namespace MicroserviceDemo.CatalogApi.Repository
{
    public class ProductRepository : CommonRepository<Product>, IProductRepository
    {
        public ProductRepository() : base(new CatalogApiDbContext())
        {
        }
    }
}