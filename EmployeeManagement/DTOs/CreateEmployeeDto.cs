namespace EmployeeManagement.DTOs
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public decimal Salary { get; set; }
    }
}
