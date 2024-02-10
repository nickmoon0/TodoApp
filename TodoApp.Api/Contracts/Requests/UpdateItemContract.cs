namespace TodoApp.Api.Contracts.Requests;

public class UpdateItemContract
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool Completed { get; set; }
}