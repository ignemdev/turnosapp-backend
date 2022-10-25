using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Queues.Application;
public static class IoC
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
