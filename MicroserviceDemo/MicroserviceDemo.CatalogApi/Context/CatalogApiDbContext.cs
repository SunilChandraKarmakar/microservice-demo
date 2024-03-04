using MongoRepo.Context;

namespace MicroserviceDemo.CatalogApi.Context
{
    public class CatalogApiDbContext : ApplicationDbContext
    {
        // Get appsettings.json file access
        private static IConfiguration configuration = new ConfigurationBuilder()
                                                     .SetBasePath(Directory.GetCurrentDirectory())
                                                     .AddJsonFile("appsettings.json", true, true)
                                                     .Build();

        // Get connection string and database name using configuration
        private static string connectionString = configuration.GetConnectionString("CatalogApi");
        private static string databaseName = configuration.GetValue<string>("DatabaseName");

        public CatalogApiDbContext() : base(connectionString, databaseName)
        {
            
        }
    }
}