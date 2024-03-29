using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application.Features;
using TodoApp.Application.Features.Auth.LoginUser;
using TodoApp.Application.Features.Auth.LogoutUser;
using TodoApp.Application.Features.Auth.RenewAccessToken;
using TodoApp.Application.Features.CreateItem;
using TodoApp.Application.Features.CreateUser;
using TodoApp.Application.Features.DeleteItem;
using TodoApp.Application.Features.GetItems;
using TodoApp.Application.Features.UpdateItem;

namespace TodoApp.Application;

public static class ServiceConfiguration
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        // User handlers
        services.AddScoped<IHandler<CreateUserCommand,CreateUserResponse>, CreateUserHandler>();
        
        // Auth handlers
        services.AddScoped<IHandler<LogoutUserCommand, LogoutUserResponse>, LogoutUserHandler>();
        services.AddScoped<IHandler<LoginUserCommand, LoginUserResponse>, LoginUserHandler>();
        services.AddScoped<IHandler<RenewAccessTokenCommand, RenewAccessTokenResponse>, RenewAccessTokenHandler>();
        
        // Item handlers
        services.AddScoped<IHandler<CreateItemCommand, CreateItemResponse>, CreateItemHandler>();
        services.AddScoped<IHandler<UpdateItemCommand, UpdateItemResponse>, UpdateItemHandler>();
        services.AddScoped<IHandler<DeleteItemCommand, DeleteItemResponse>, DeleteItemHandler>();
        services.AddScoped<IHandler<GetItemsCommand, GetItemsResponse>, GetItemsHandler>();
        
        return services;
    }
}