using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IBonusCalculator
    {
        decimal CalculateBonus(Employee employee);
    }
}
