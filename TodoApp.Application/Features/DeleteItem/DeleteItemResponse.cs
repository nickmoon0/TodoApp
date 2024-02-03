namespace TodoApp.Application.Features.DeleteItem;

public class DeleteItemResponse : IResponse
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
}