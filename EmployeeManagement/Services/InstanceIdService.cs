namespace EmployeeManagement.Services
{
    public class InstanceIdService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
    }
}