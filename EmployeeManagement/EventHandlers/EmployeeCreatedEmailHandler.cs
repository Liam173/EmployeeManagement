using EmployeeManagement.Events;
using EmployeeManagement.Interfaces;

namespace EmployeeManagement.EventHandlers;

public class EmployeeCreatedEmailHandler
    : IEventHandler<EmployeeCreatedEvent>
{
    private readonly ILogger<EmployeeCreatedEmailHandler> _logger;

    public EmployeeCreatedEmailHandler(
        ILogger<EmployeeCreatedEmailHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(EmployeeCreatedEvent @event)
    {
        _logger.LogInformation(
            "Sending welcome email to {Employee}.",
            @event.EmployeeName);

        return Task.CompletedTask;
    }
}