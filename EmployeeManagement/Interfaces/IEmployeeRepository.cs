using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();

        Employee? GetById(int id);

        void Add(Employee employee);
    }
}
