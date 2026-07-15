namespace EmployeeManagement.DTOs.V2
{
    public class CreateEmployeeV2Dto
    {
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public decimal Salary { get; set; }

        public string Department { get; set; } = string.Empty;
    }
}
