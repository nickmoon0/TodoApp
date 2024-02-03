using TodoApp.Application.Models;

namespace TodoApp.Application.Features.CreateItem;

public class CreateItemResponse : IResponse
{
    public Item? CreatedItem { get; set; }
    public bool Success { get; set; }
    public int StatusCode { get; set; }
}