using EmployeeManagement.Events;
using EmployeeManagement.Interfaces;

namespace EmployeeManagement.EventHandlers;

public class EmployeeCreatedAuditHandler
    : IEventHandler<EmployeeCreatedEvent>
{
    private readonly ILogger<EmployeeCreatedAuditHandler> _logger;

    public EmployeeCreatedAuditHandler(
        ILogger<EmployeeCreatedAuditHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(EmployeeCreatedEvent @event)
    {
        _logger.LogInformation(
            "Audit: Employee {EmployeeId} created.",
            @event.EmployeeId);

        return Task.CompletedTask;
    }
}