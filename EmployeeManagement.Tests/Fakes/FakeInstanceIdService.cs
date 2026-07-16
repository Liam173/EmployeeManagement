using EmployeeManagement.Interfaces;
using EmployeeManagement.Services;

public class FakeInstanceIdService : IInstanceIdService
{
    public Guid InstanceId { get; }
        = Guid.Parse(
            "11111111-1111-1111-1111-111111111111");
}