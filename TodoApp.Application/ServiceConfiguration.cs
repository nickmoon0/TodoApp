using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application.Features;
using TodoApp.Application.Features.CreateUser;
using TodoApp.Application.Features.LoginUser;

namespace TodoApp.Application;

public static class ServiceConfiguration
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IHandler<CreateUserCommand,CreateUserResponse>, CreateUserHandler>();
        services.AddScoped<IHandler<LoginUserCommand, LoginUserResponse>, LoginUserHandler>();
        
        return services;
    }
}