using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Applications.Contracts.Infrastructure.EmailServices;
using Ordering.Applications.Contracts.Persistence.OrderRepositories;
using Ordering.Infrastructure.EmailService;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories.OrderRepository;

namespace Ordering.Infrastructure.Utilities.ServiceRegistration
{
    public static class OrderingInfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<OrdringDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IEmailService, SendEmailService>();
            return services;
        }
    }
}