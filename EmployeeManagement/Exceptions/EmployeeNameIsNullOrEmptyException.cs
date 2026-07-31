namespace EmployeeManagement.Exceptions
{
    public class EmployeeNameIsNullOrEmptyException : Exception
    {
        public EmployeeNameIsNullOrEmptyException()
            : base($"Employee name must be supplied, it cannot be null or empty.")
        { }
    }
}
