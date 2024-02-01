using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application.Features;
using TodoApp.Application.Features.CreateUser;

namespace TodoApp.Application;

public static class ServiceConfiguration
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IHandler<CreateUserCommand,CreateUserResponse>, CreateUserHandler>();
        
        return services;
    }
}