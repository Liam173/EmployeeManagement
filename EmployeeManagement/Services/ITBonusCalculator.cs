using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public class ITBonusCalculator : IBonusCalculator
    {
        public string Department => "IT";

        public decimal CalculateBonus(Employee employee)
        {
            // Calculate bonus based on IT department criteria
            decimal bonus = employee.Salary * 0.10m; // 10% of salary as bonus
            return bonus;
        }
    }
}
