using AutoMapper;
using EmployeeManagement.DTOs;
using EmployeeManagement.Exceptions;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace EmployeeManagement.Tests.UnitTests;

public class EmployeeServiceTests
{
    private readonly Mock<IEmployeeRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<EmployeeService>> _loggerMock;
    private readonly Mock<IEventPublisher> _eventPublisherMock;

    private readonly MemoryCache _memoryCache;

    private readonly EmployeeService _service;

    public EmployeeServiceTests()
    {
        _repositoryMock = new Mock<IEmployeeRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<EmployeeService>>();
        _eventPublisherMock = new Mock<IEventPublisher>();

        _memoryCache = new MemoryCache(new MemoryCacheOptions());

        _service = new EmployeeService(
            _repositoryMock.Object,
            _mapperMock.Object,
            _loggerMock.Object,
            _memoryCache,
            _eventPublisherMock.Object);
    }

    private List<Employee> CreateEmployeeList()
    {
        return new List<Employee>
        {
            new Employee { Id = 1, Name = "Liam", Age = 28, Salary = 50000 },
            new Employee { Id = 2, Name = "John", Age = 54, Salary = 30000 }
        };
    }

    private List<EmployeeDto> CreateEmployeeDtoList()
    {
        return new List<EmployeeDto>
        {
            new EmployeeDto { Id = 1, Name = "Liam", Age = 28 },
            new EmployeeDto { Id = 2, Name = "John", Age = 54 }
        };
    }

    private Employee CreateEmployee()
    {
        return new Employee
        {
            Id = 1,
            Name = "Liam",
            Age = 28,
            Salary = 50000
        };
    }

    private EmployeeDto CreateEmployeeDto()
    {
        return new EmployeeDto
        {
            Id = 1,
            Name = "Liam",
            Age = 28
        };
    }

    [Fact]
    public void GetAllEmployees_ShouldReturnEmployeeDtoList_WhenEmployeesExists() 
    {
        // Arrange

        var employees = CreateEmployeeList();
        var employeeDtos = CreateEmployeeDtoList();

        _repositoryMock
            .Setup(x => x.GetAll())
            .Returns(employees);

        _mapperMock
            .Setup(x => x.Map<List<EmployeeDto>>(employees))
            .Returns(employeeDtos);

        // Act

        var result = _service.GetAllEmployees();

        // Assert

        var cached = _memoryCache.Get<List<EmployeeDto>>(
            EmployeeService.CacheKeys.AllEmployees);

        Assert.NotNull(cached);
        Assert.Equal(employeeDtos.Count, cached.Count);
        Assert.Equal(employeeDtos[0].Id, cached[0]!.Id);
        Assert.Equal(employeeDtos[1].Id, cached[1]!.Id);

        Assert.NotNull(result);
        Assert.Equal(employeeDtos.Count, result.Count);
        Assert.Collection(result,
            first =>
            {
                Assert.Equal(employeeDtos[0].Id, first.Id);
                Assert.Equal(employeeDtos[0].Name, first.Name);
                Assert.Equal(employeeDtos[0].Age, first.Age);
            },
            second =>
            {
                Assert.Equal(employeeDtos[1].Id, second.Id);
                Assert.Equal(employeeDtos[1].Name, second.Name);
                Assert.Equal(employeeDtos[1].Age, second.Age);
            });

        // Verify

        _repositoryMock.Verify(x => x.GetAll(), Times.Once);
        _mapperMock.Verify(x => x.Map<List<EmployeeDto>>(employees), Times.Once);
    }

    [Fact]
    public void GetAllEmployees_ShouldReturnEmployeeDtoListFromCache_WhenEmployeeExistsInCache()
    {
        // Arrange

        var employeeDtos = CreateEmployeeDtoList();

        _memoryCache.Set(
            EmployeeService.CacheKeys.AllEmployees,
            employeeDtos);

        // Act

        var result = _service.GetAllEmployees();

        // Assert

        Assert.NotNull(result);
        Assert.Equal(employeeDtos[0].Id, result[0].Id);

        // Verify

        _repositoryMock.Verify(
            x => x.GetAll(),
            Times.Never);

        _mapperMock.Verify(
            x => x.Map<List<EmployeeDto>>(It.IsAny<List<Employee>>()),
            Times.Never);
    }

    [Fact]
    public void GetEmployee_ShouldReturnEmployeeDto_WhenEmployeeExists()
    {
        // Arrange

        var employee = CreateEmployee();
        var employeeDto = CreateEmployeeDto();

        _repositoryMock
            .Setup(x => x.GetById(1))
            .Returns(employee);

        _mapperMock
            .Setup(x => x.Map<EmployeeDto>(employee))
            .Returns(employeeDto);

        // Act

        var result = _service.GetEmployeeById(1);

        // Assert

        var cached = _memoryCache.Get<EmployeeDto>(
            EmployeeService.CacheKeys.Employee(1));

        Assert.NotNull(cached);
        Assert.Equal(employeeDto.Id, cached!.Id);
        Assert.NotNull(result);
        Assert.Equal(employeeDto.Id, result.Id);
        Assert.Equal(employeeDto.Name, result.Name);
        Assert.Equal(employeeDto.Age, result.Age);

        // Verify

        _repositoryMock.Verify(x => x.GetById(1), Times.Once);
        _mapperMock.Verify(x => x.Map<EmployeeDto>(employee), Times.Once);
    }

    [Fact]
    public void GetEmployee_ShouldThrowEmployeeNotFoundException_WhenEmployeeDoesNotExist()
    {
        // Arrange

        _repositoryMock
            .Setup(x => x.GetById(1))
            .Returns((Employee?)null);

        // Act & Assert

        Assert.Throws<EmployeeNotFoundException>(
            () => _service.GetEmployeeById(1));

        // Verify

        _repositoryMock.Verify(x => x.GetById(1), Times.Once);

        _mapperMock.Verify(
            x => x.Map<EmployeeDto>(It.IsAny<Employee>()),
            Times.Never);
    }

    [Fact]
    public void GetEmployee_ShouldReturnEmployeeFromCache_WhenEmployeeExistsInCache()
    {
        // Arrange

        var employeeDto = CreateEmployeeDto();

        _memoryCache.Set(
            EmployeeService.CacheKeys.Employee(1),
            employeeDto);

        // Act

        var result = _service.GetEmployeeById(1);

        // Assert

        Assert.NotNull(result);
        Assert.Equal(employeeDto.Id, result.Id);

        // Verify

        _repositoryMock.Verify(
            x => x.GetById(It.IsAny<int>()),
            Times.Never);

        _mapperMock.Verify(
            x => x.Map<EmployeeDto>(It.IsAny<Employee>()),
            Times.Never);
    }
}