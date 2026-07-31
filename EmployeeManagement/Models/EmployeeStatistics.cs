namespace EmployeeManagement.Models
{
    public class EmployeeStatistics
    {
        public int ActiveEmployeeCount { get; set; }

        public decimal AverageSalary { get; set; }

        public EmployeePractice HighestPaidEmployee { get; set; }

        public Dictionary<string, int> EmployeesPerDepartment { get; set; }
    }
}
