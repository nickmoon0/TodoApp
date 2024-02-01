namespace TodoApp.Application.Features;

public interface IResponse
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
}