using MicroserviceDemo.CatalogApi.Context;

namespace MicroserviceDemo.CatalogApi.HostingService
{
    public class CatalogApiHostingService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            CatalogApiDbContextSeedData.Seed();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}