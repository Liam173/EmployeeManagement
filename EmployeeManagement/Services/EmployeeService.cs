using AutoMapper;
using EmployeeManagement.DTOs;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeService(
            IEmployeeRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<EmployeeDto> GetAllEmployees()
        {
            var employees = _repository.GetAll();

            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public EmployeeDto? GetEmployeeById(int id)
        {
            var employee = _repository.GetById(id);

            if (employee == null)
                return null;

            return _mapper.Map<EmployeeDto>(employee);
        }

        public bool ValidateCreateEmployee(CreateEmployeeDto employee)
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
            _repository.Add(_mapper.Map<Employee>(dto));
        }

        public void UpdateEmployee(int id, UpdateEmployeeDto dto) 
        {
            _repository.Update(id, _mapper.Map<Employee>(dto));
        }

        public void DeleteEmployee(int id)
        {
            _repository.Delete(id);
        }
    }
}
