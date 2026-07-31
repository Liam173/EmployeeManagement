namespace EmployeeManagement.Exceptions
{
    public class EmployeeSalaryMustBeSpecifiedExcpetion : Exception
    {
        public EmployeeSalaryMustBeSpecifiedExcpetion()
            : base($"Employee salary must be higher than 0.")
        { }
    }
}
