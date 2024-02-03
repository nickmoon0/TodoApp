namespace TodoApp.Application.Features.CreateItem;

public class CreateItemCommand
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool Completed { get; set; }
}