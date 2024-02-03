using TodoApp.Application.Models;

namespace TodoApp.Application.Features.GetItems;

public class GetItemsResponse : IResponse
{
    public ICollection<Item> Items { get; set; } = null!;
    public bool Success { get; set; }
    public int StatusCode { get; set; }
}