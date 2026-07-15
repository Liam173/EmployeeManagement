namespace EmployeeManagement.Events;

public class EmployeeCreatedEvent
{
    public int EmployeeId { get; }

    public string EmployeeName { get; }

    public EmployeeCreatedEvent(int employeeId, string employeeName)
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;
    }
}