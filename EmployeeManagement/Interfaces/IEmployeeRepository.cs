using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll(int tenantId);

        Employee? GetById(int id);

        void Add(Employee employee);

        void SaveChanges();

        void Delete(Employee employee);
    }
}
