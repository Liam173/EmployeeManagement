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

        public List<Employee> GetAllEmployees()
        {
            return _repository.GetAll();
        }

        public Employee? GetEmployeeById(int id)
        {
            return _repository.GetById(id);
        }

        public bool ValidateEmployee(Employee employee)
        {
            return _repository.Validate(employee);
        }

        public void AddEmployee(Employee employee) 
        {
            _repository.Add(employee);
        }

        public void UpdateEmployee(int id, Employee employee) 
        {
            _repository.Update(id, employee);
        }
    }
}
