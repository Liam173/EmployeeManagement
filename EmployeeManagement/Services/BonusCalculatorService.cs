using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public class BonusCalculatorService
    {
        private readonly IEnumerable<IBonusCalculator> _bonusCalculator;

        public BonusCalculatorService(IEnumerable<IBonusCalculator> bonusCalculator)
        {
            _bonusCalculator = bonusCalculator;
        }

        public decimal CalculateBonus(Employee employee)
        {
            foreach (var calculator in _bonusCalculator)
            {
                if (employee.Department.Equals(calculator.GetType))
                {
                    return calculator.CalculateBonus(employee);
                }
            }

            return 0m;
        }
    }
}
