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
            if (employee.Id < 0)
                return false;

            if (string.IsNullOrEmpty(employee.Name))
                return false;

            if (employee.Age < 18)
                return false;

            if (employee.Salary < 0)
                return false;

            return true;
        }

        public void AddEmployee(Employee employee) 
        {
            _repository.Add(employee);
        }

        public void UpdateEmployee(int id, Employee employee) 
        {
            _repository.Update(id, employee);
        }

        public void DeleteEmployee(int id)
        {
            _repository.Delete(id);
        }
    }
}
