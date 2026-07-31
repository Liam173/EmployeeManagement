namespace EmployeeManagement.DTOs
{
    public class SearchEmployeeDto
    {
        public string? Name { get; set; }

        public string? Department { get; set; }

        public decimal? MinimumSalary { get; set; }

        public bool? IsActive { get; set; }
    }
}
