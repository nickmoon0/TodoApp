using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application.Common.Repositories;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Infrastructure;

public static class ServiceConfiguration
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        
        return services;
    }
}