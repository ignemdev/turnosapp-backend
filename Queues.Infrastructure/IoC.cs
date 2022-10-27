using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Queues.Application.Interfaces;
using Queues.Infrastructure.Context;
using Queues.Infrastructure.Services;

namespace Queues.Infrastructure;
public static class IoC
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IQueueService, QueueService>();
        services.AddTransient<IPersonQueueService, PersonQueueService>();
        services.AddTransient<IPersonService, PersonService>();
        services.AddTransient<IDocumentRecognizerService, DocumentRecognizerService>();

        services.AddTransient<IQueuesDbContext, QueuesDbContext>();

        services.AddDbContext<QueuesDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
