namespace TodoApp.Api.Contracts.Requests;

public class CreateItemContract
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool Completed { get; set; }
}