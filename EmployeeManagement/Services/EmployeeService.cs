using AutoMapper;
using EmployeeManagement.DTOs;
using EmployeeManagement.Events;
using EmployeeManagement.Exceptions;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.Extensions.Caching.Memory;

namespace EmployeeManagement.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;
        private readonly IMemoryCache _cache;
        private readonly IEventPublisher _eventPublisher;

        public static class CacheKeys
        {
            public const string AllEmployees = "Employees_All";

            public static string Employee(int id)
            {
                return $"Employee_{id}";
            }
        }

        public EmployeeService(
            IEmployeeRepository repository,
            IMapper mapper,
            ILogger<EmployeeService> logger,
            IMemoryCache cache,
            IEventPublisher eventPublisher)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
            _eventPublisher = eventPublisher;
        }

        public List<EmployeeDto> GetAllEmployees()
        {
            _logger.LogInformation(
                "Retrieving all employees.");

            if (_cache.TryGetValue(CacheKeys.AllEmployees, out List<EmployeeDto>? cachedEmployees))
            {
                _logger.LogInformation("Returned all employees from cache.");

                return cachedEmployees!;
            }

            var employees = _repository.GetAll();

            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

            _cache.Set(
                CacheKeys.AllEmployees,
                employeeDtos,
                TimeSpan.FromMinutes(5));

            _logger.LogInformation("Stored all employees in cache.");

            return employeeDtos;
        }

        public EmployeeDto GetEmployeeById(int id)
        {
            _logger.LogInformation(
                "Retrieving employee {EmployeeId}.",
                id);

            var cacheKey = CacheKeys.Employee(id);

            if (_cache.TryGetValue(cacheKey, out EmployeeDto? cachedEmployee))
            {
                _logger.LogInformation(
                    "Employee {EmployeeId} returned from cache.",
                    id);

                return cachedEmployee!;
            }

            var employee = _repository.GetById(id);

            if (employee == null)
            {
                _logger.LogWarning(
                    "Employee {EmployeeId} was not found.",
                    id);

                throw new EmployeeNotFoundException(id);
            }

            var dto = _mapper.Map<EmployeeDto>(employee);

            _cache.Set(
                cacheKey,
                dto,
                TimeSpan.FromMinutes(5));

            _logger.LogInformation(
                "Employee {EmployeeId} stored in cache.",
                id);

            return dto;
        }

        public async Task AddEmployee(CreateEmployeeDto dto) 
        {
            _logger.LogInformation(
                "Creating new employee.");

            var employee = _mapper.Map<Employee>(dto);

            _repository.Add(employee);

            _logger.LogInformation(
                "Employee was added successfully.");

            _cache.Remove(CacheKeys.AllEmployees);

            _logger.LogInformation(
                "Employee cache removed.");

            await _eventPublisher.PublishAsync(
                new EmployeeCreatedEvent(
                    employee.Id,
                    employee.Name));
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

            _repository.SaveChanges();

            _logger.LogInformation(
                "Employee {EmployeeId} was updated successfully.",
                id);

            _cache.Remove(CacheKeys.Employee(id));
            _cache.Remove(CacheKeys.AllEmployees);

            _logger.LogInformation(
                "Employee {EmployeeId} cache removed.",
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

            _cache.Remove(CacheKeys.Employee(id));
            _cache.Remove(CacheKeys.AllEmployees);

            _logger.LogInformation(
                "Employee {EmployeeId} cache removed.",
                id);
        }
    }
}
