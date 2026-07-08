using EmployeeManagement.DTOs;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public List<EmployeeDto> GetAllEmployees()
        {
            var employees = _repository.GetAll();

            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                Age = e.Age
            }).ToList();
        }

        public EmployeeDto? GetEmployeeById(int id)
        {
            var employee = _repository.GetById(id);

            if (employee == null)
                return null;

            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age
            };
        }

        public bool ValidateEmployee(CreateEmployeeDto employee)
        {
            if (employee == null)
                return false;

            if (string.IsNullOrWhiteSpace(employee.Name))
                return false;

            if (employee.Age < 18)
                return false;

            if (employee.Salary <= 0)
                return false;

            return true;
        }

        public void AddEmployee(CreateEmployeeDto dto) 
        {
            var employee = new Employee
            {
                Name = dto.Name,
                Age = dto.Age,
                Salary = dto.Salary
            };

            _repository.Add(employee);
        }

        public void UpdateEmployee(int id, UpdateEmployeeDto dto) 
        {
            var employee = new Employee
            {
                Name = dto.Name,
                Age = dto.Age,
                Salary = dto.Salary
            };

            _repository.Update(id, employee);
        }

        public void DeleteEmployee(int id)
        {
            _repository.Delete(id);
        }
    }
}
