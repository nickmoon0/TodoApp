using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Contracts;
using TodoApp.Application.Features;
using TodoApp.Application.Features.CreateItem;

namespace TodoApp.Api.Services;

public interface IItemService
{
    public Task<IResult> CreateItem(
        [FromBody] CreateItemContract contract,
        [FromServices] IHandler<CreateItemCommand, CreateItemResponse> handler,
        HttpContext context);
}