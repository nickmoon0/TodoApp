using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Contracts;
using TodoApp.Application.Features;
using TodoApp.Application.Features.CreateItem;
using TodoApp.Application.Features.DeleteItem;
using TodoApp.Application.Features.UpdateItem;

namespace TodoApp.Api.Services;

public interface IItemService
{
    public Task<IResult> CreateItem(
        [FromBody] CreateItemContract contract,
        [FromServices] IHandler<CreateItemCommand, CreateItemResponse> handler,
        HttpContext context);

    public Task<IResult> UpdateItem(
        [FromRoute] Guid itemId,
        [FromBody] UpdateItemContract contract,
        [FromServices] IHandler<UpdateItemCommand, UpdateItemResponse> handler,
        HttpContext context);

    public Task<IResult> DeleteItem(
        [FromRoute] Guid itemId,
        [FromServices] IHandler<DeleteItemCommand, DeleteItemResponse> handler,
        HttpContext context);
}