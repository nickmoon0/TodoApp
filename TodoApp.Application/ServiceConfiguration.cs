using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application.Features;
using TodoApp.Application.Features.CreateItem;
using TodoApp.Application.Features.CreateUser;
using TodoApp.Application.Features.LoginUser;

namespace TodoApp.Application;

public static class ServiceConfiguration
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        // User handlers
        services.AddScoped<IHandler<CreateUserCommand,CreateUserResponse>, CreateUserHandler>();
        
        // Auth handlers
        services.AddScoped<IHandler<LoginUserCommand, LoginUserResponse>, LoginUserHandler>();
        
        // Item handlers
        services.AddScoped<IHandler<CreateItemCommand, CreateItemResponse>, CreateItemHandler>();
        
        return services;
    }
}