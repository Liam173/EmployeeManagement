using AutoMapper;
using EmployeeManagement.DTOs;
using EmployeeManagement.Exceptions;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(
            IEmployeeRepository repository,
            IMapper mapper,
            ILogger<EmployeeService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public List<EmployeeDto> GetAllEmployees()
        {
            _logger.LogInformation(
                "Attempting to get all employees.");

            var employees = _repository.GetAll();

            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public EmployeeDto GetEmployeeById(int id)
        {
            _logger.LogInformation(
                "Attempting to get employee {EmployeeId}.",
                id);

            var employee = _repository.GetById(id);

            if (employee == null)
            {
                _logger.LogWarning(
                    "Employee {EmployeeId} was not found.",
                    id);

                throw new EmployeeNotFoundException(id);
            }

            return _mapper.Map<EmployeeDto>(employee);
        }

        public void AddEmployee(CreateEmployeeDto dto) 
        {
            _logger.LogInformation(
                "Attempting to add new employee.");

            _repository.Add(_mapper.Map<Employee>(dto));

            _logger.LogInformation(
                "Employee was added succesfully.");
        }

        public void UpdateEmployee(int id, UpdateEmployeeDto dto) 
        {
            _logger.LogInformation(
                "Attempting to update employee {EmployeeId}.",
                id);

            var existingEmployee = _repository.GetById(id);

            if (existingEmployee == null)
            {
                _logger.LogWarning(
                    "Employee {EmployeeId} was not found.",
                    id);

                throw new EmployeeNotFoundException(id);
            }

            existingEmployee.Name = dto.Name;
            existingEmployee.Age = dto.Age;
            existingEmployee.Salary = dto.Salary;

            _repository.Update();

            _logger.LogInformation(
                "Employee {EmployeeId} was updated successfully.",
                id);
        }

        public void DeleteEmployee(int id)
        {
            _logger.LogInformation(
                "Attempting to delete employee {EmployeeId}.",
                id);

            var employee = _repository.GetById(id);

            if (employee == null)
            {
                _logger.LogWarning(
                    "Employee {EmployeeId} was not found.",
                    id);

                throw new EmployeeNotFoundException(id);
            }

            _repository.Delete(employee);

            _logger.LogInformation(
                "Employee {EmployeeId} deleted successfully.",
                id);
        }
    }
}
