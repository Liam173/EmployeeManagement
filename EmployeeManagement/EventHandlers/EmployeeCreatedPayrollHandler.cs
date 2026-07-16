using EmployeeManagement.Events;
using EmployeeManagement.Interfaces;

namespace EmployeeManagement.EventHandlers
{
    public class EmployeeCreatedPayrollHandler
    : IEventHandler<EmployeeCreatedEvent>
    {
        private readonly PayrollClient _client;

        public EmployeeCreatedPayrollHandler(
            PayrollClient client)
        {
            _client = client;
        }

        public async Task HandleAsync(
            EmployeeCreatedEvent @event)
        {
            await _client.NotifyEmployeeCreatedAsync(
                @event.EmployeeId);
        }
    }
}
