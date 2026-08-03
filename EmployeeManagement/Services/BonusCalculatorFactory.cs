using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public class BonusCalculatorFactory
    {
        private readonly IEnumerable<IBonusCalculator> _bonusCalculators;

        public BonusCalculatorFactory(IEnumerable<IBonusCalculator> bonusCalculator)
        {
            _bonusCalculators = bonusCalculator;
        }

        public decimal CalculateBonus(Employee employee)
        {
            var calculator =
                _bonusCalculators
                    .FirstOrDefault(x =>
                        x.Department == employee.Department);

            if (calculator == null)
                throw new InvalidOperationException(
                    "No bonus calculator registered.");

            return calculator.CalculateBonus(employee);
        }
    }
}
