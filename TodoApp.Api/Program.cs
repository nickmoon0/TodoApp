using TodoApp.Application;
using TodoApp.Api;
using TodoApp.Api.Endpoints;
using TodoApp.Infrastructure;
using TodoApp.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add additional config files
builder.Configuration.AddJsonFile("appsettings.Local.json");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterInfrastructure();
builder.Services.RegisterApplicationServices();
builder.Services.RegisterApiServices();

builder.RegisterSettings();
builder.ConfigureAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.RegisterUserEndpoints("/user");
app.RegisterAuthEndpoints("/auth");
app.RegisterItemEndpoints("/item");

app.Run();