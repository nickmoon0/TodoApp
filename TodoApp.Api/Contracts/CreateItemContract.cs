namespace TodoApp.Api.Contracts;

public class CreateItemContract
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool Completed { get; set; }
}