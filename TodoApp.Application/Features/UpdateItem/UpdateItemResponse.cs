using TodoApp.Application.Models;

namespace TodoApp.Application.Features.UpdateItem;

public class UpdateItemResponse : IResponse
{
    public Item? UpdatedItem { get; set; }
    public bool Success { get; set; }
    public int StatusCode { get; set; }
}