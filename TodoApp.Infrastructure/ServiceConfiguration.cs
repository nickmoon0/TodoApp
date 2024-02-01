using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application.Common;
using TodoApp.Application.Common.Repositories;
using TodoApp.Infrastructure.Data;
using TodoApp.Infrastructure.Services;

namespace TodoApp.Infrastructure;

public static class ServiceConfiguration
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        
        services.AddTransient<ITokenService, TokenService>();
        
        return services;
    }
}