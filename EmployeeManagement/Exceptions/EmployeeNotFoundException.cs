namespace EmployeeManagement.Exceptions
{
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException(int id)
            : base($"Employee {id} was not found.")
        { }
    }
}
