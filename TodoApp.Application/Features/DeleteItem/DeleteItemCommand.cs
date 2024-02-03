namespace TodoApp.Application.Features.DeleteItem;

public class DeleteItemCommand
{
    public Guid ItemId { get; set; }
    public Guid UserId { get; set; }
}