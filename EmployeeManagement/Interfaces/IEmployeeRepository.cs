using EmployeeManagement.DTOs;
using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll(int tenantId);

        Employee? GetById(int id, int tenantId);

        List<Employee> SearchEmployees(int tenantId, SearchEmployeeDto dto);

        void Add(Employee employee);

        void SaveChanges();

        void Delete(Employee employee);
    }
}
