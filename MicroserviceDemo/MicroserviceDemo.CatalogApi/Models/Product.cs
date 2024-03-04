using MongoDB.Bson.Serialization.Attributes;

namespace MicroserviceDemo.CatalogApi.Models
{
    public class Product
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageFile { get; set; }
    }
}