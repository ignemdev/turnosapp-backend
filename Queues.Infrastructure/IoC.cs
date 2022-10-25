using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Queues.Infrastructure.Context;

namespace Queues.Infrastructure;
public static class IoC
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<QueuesDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
