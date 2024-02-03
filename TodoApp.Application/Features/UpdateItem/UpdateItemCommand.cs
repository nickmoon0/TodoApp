namespace TodoApp.Application.Features.UpdateItem;

public class UpdateItemCommand
{
    public required Guid UserId { get; set; }
    public required Guid ItemId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool Completed { get; set; }
}