using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IBonusCalculator
    {
        string Department { get; }

        decimal CalculateBonus(Employee employee);
    }
}
