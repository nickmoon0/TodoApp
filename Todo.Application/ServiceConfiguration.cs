using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Data.Repositories;
using Todo.Application.Features;
using Todo.Application.Features.CreateUser;

namespace Todo.Application;

public static class ServiceConfiguration
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();

        services.AddScoped<IHandler<CreateUserCommand,CreateUserResponse>, CreateUserHandler>();
        
        return services;
    }
}