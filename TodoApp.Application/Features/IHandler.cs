namespace TodoApp.Application.Features;

public interface IHandler<in TCommand, TResponse>
{
    public Task<TResponse> Handle(TCommand command);
}