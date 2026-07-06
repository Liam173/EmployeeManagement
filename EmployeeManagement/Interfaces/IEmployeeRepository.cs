using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();

        Employee? GetById(int id);

        bool Validate(Employee employee);

        void Add(Employee employee);

        void Update(int id, Employee employee);
    }
}
