namespace EmployeeManagement.Exceptions
{
    public class EmployeeUnderAgeException : Exception
    {
        public EmployeeUnderAgeException()
            : base($"Employee must be older than 18.")
        { }
    }
}
