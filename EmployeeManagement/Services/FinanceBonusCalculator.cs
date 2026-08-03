using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public class FinanceBonusCalculator : IBonusCalculator
    {
        public decimal CalculateBonus(Employee employee)
        {
            // Calculate bonus based on Finance department criteria
            decimal bonus = employee.Salary * 0.20m; // 20% of salary as bonus
            return bonus;
        }
    }
}
