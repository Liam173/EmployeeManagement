using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees =
        [
            new Employee
            {
                Id = 1,
                Name = "John",
                Age = 30,
                Salary = 40000
            },

            new Employee
            {
                Id = 2,
                Name = "Alice",
                Age = 27,
                Salary = 50000
            }
        ];

        public List<Employee> GetAll()
        {
            return _employees;
        }

        public Employee? GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public void Add(Employee employee)
        {
            _employees.Add(employee);
        }
    }
}