using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public class HRBonusCalculator : IBonusCalculator
    {
        public string Department => "HR";

        public decimal CalculateBonus(Employee employee)
        {
            // Calculate bonus based on HR department criteria
            decimal bonus = employee.Salary * 0.05m; // 5% of salary as bonus
            return bonus;
        }
    }
}
