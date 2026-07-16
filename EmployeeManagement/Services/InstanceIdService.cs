using EmployeeManagement.Interfaces;

namespace EmployeeManagement.Services
{
    public class InstanceIdService : IInstanceIdService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
    }
}