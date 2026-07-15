namespace EmployeeManagement.Interfaces;

public interface IEventHandler<TEvent>
{
    Task HandleAsync(TEvent @event);
}