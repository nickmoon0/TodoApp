using TodoApp.Api.Services;

namespace TodoApp.Api;

public static class ServiceConfiguration
{
    public static IServiceCollection RegisterApiServices(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        
        return services;
    }
}