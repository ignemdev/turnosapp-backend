using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Queues.Application.Person.Handlers;
using Queues.Application.PersonQueue.Handlers;
using Queues.Application.Queue.Handlers;
using System.Reflection;

namespace Queues.Application;

public static class IoC
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IQueueHandler, QueueHandler>();
        services.AddTransient<IPersonQueueHandler, PersonQueueHandler>();
        services.AddTransient<IPersonHandler, PersonHandler>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}