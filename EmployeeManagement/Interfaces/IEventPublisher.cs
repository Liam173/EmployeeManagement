namespace EmployeeManagement.Interfaces;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent @event);
}