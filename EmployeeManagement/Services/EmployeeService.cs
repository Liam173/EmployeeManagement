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
        private readonly ITenantProvider _tenantProvider;

        public static class CacheKeys
        {
            public static string AllEmployees(int tenantId)
                => $"AllEmployees_{tenantId}";

            public static string Employee(int id, int tenantId)
            {
                return $"Tenant{tenantId}_Employee{id}";
            }
        }

        public EmployeeService(
            IEmployeeRepository repository,
            IMapper mapper,
            ILogger<EmployeeService> logger,
            IMemoryCache cache,
            IEventPublisher eventPublisher,
            ITenantProvider tenantProvider)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
            _eventPublisher = eventPublisher;
            _tenantProvider = tenantProvider;
        }

        public List<EmployeeDto> GetAllEmployees()
        {
            _logger.LogInformation(
                "Retrieving all employees.");

            if (_cache.TryGetValue(CacheKeys.AllEmployees(_tenantProvider.TenantId), out List<EmployeeDto>? cachedEmployees))
            {
                _logger.LogInformation("Returned all employees from cache.");

                return cachedEmployees!;
            }

            var employees = _repository.GetAll(_tenantProvider.TenantId);

            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

            _cache.Set(
                CacheKeys.AllEmployees(_tenantProvider.TenantId),
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

            var cacheKey = CacheKeys.Employee(id, _tenantProvider.TenantId);

            if (_cache.TryGetValue(cacheKey, out EmployeeDto? cachedEmployee))
            {
                _logger.LogInformation(
                    "Employee {EmployeeId} returned from cache.",
                    id);

                return cachedEmployee!;
            }

            var employee = _repository.GetById(id, _tenantProvider.TenantId);

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

        public async Task<List<EmployeeDto>> SearchEmployees(SearchEmployeeDto dto)
        {
            var employees = _repository.SearchEmployees(_tenantProvider.TenantId, dto);

            var employeeDtos = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                employeeDtos.Add(new EmployeeDto
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age
                });
            }

            return employeeDtos;
        }

        public async Task AddEmployee(CreateEmployeeDto dto)
        {
            _logger.LogInformation(
                "Creating new employee.");

            var employee = _mapper.Map<Employee>(dto);

            employee.TenantId = _tenantProvider.TenantId;

            _repository.Add(employee);

            _logger.LogInformation(
                "Employee was added successfully.");

            _cache.Remove(CacheKeys.AllEmployees(_tenantProvider.TenantId));

            _logger.LogInformation(
                "Employee cache removed.");

            await _eventPublisher.PublishAsync(
                new EmployeeCreatedEvent(
                    employee.Id,
                    employee.Name));
        }

        public async Task CreateEmployee(CreateEmployeeDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new EmployeeNameIsNullOrEmptyException();

            if (dto.Age < 18)
                throw new EmployeeUnderAgeException();

            if (dto.Salary <= 0)
                throw new EmployeeSalaryMustBeSpecifiedExcpetion();

            // I don't have department in my dto, but would implement same as name validation.

            var employee = new Employee
            {
                Name = dto.Name,
                Age = dto.Age,
                Salary = dto.Salary,
                TenantId = _tenantProvider.TenantId
            };

            _repository.Add(employee);
        }

        public void UpdateEmployee(int id, UpdateEmployeeDto dto)
        {
            _logger.LogInformation(
                "Attempting to update employee {EmployeeId}.",
                id);

            var existingEmployee = _repository.GetById(id, _tenantProvider.TenantId);

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

            _cache.Remove(CacheKeys.Employee(id, _tenantProvider.TenantId));
            _cache.Remove(CacheKeys.AllEmployees(_tenantProvider.TenantId));

            _logger.LogInformation(
                "Employee {EmployeeId} cache removed.",
                id);
        }

        public void DeleteEmployee(int id)
        {
            _logger.LogInformation(
                "Attempting to delete employee {EmployeeId}.",
                id);

            var employee = _repository.GetById(id, _tenantProvider.TenantId);

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

            _cache.Remove(CacheKeys.Employee(id, _tenantProvider.TenantId));
            _cache.Remove(CacheKeys.AllEmployees(_tenantProvider.TenantId));

            _logger.LogInformation(
                "Employee {EmployeeId} cache removed.",
                id);
        }

        #region "Practice work"

        public EmployeeStatistics GetStatistics(List<EmployeePractice> employees)
        {
            var activeEmployees = employees
                .Where(x => x.IsActive)
                .ToList();

            return new EmployeeStatistics
            {
                ActiveEmployeeCount = activeEmployees.Count,

                AverageSalary = activeEmployees.Any()
                    ? activeEmployees.Average(x => x.Salary)
                    : 0,

                HighestPaidEmployee = activeEmployees
                    .OrderByDescending(x => x.Salary)
                    .FirstOrDefault(),

                EmployeesPerDepartment = employees
                    .GroupBy(x => x.Department)
                    .ToDictionary(g => g.Key, g => g.Count())
            };
        }

        #endregion
    }
}
