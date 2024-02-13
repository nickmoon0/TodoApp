using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application.Common;
using TodoApp.Application.Common.Repositories;
using TodoApp.Infrastructure.Data;
using TodoApp.Infrastructure.Services;
using TodoApp.Infrastructure.Settings;

namespace TodoApp.Infrastructure;

public static class ServiceConfiguration
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IItemRepository, ItemRepository>();
        services.AddTransient<ITokenRepository, TokenRepository>();
        
        services.AddTransient<ITokenService, TokenService>();
        
        return services;
    }

    public static WebApplicationBuilder RegisterSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<MongoDbSettings>(
            builder.Configuration.GetSection(nameof(MongoDbSettings)));
        builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection(nameof(JwtSettings)));
        
        return builder;
    }
}