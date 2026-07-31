using EmployeeManagement.Data;
using EmployeeManagement.DTOs;
using EmployeeManagement.Exceptions;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Employee> GetAll(int tenantId)
        {
            return _context.Employees
                .AsNoTracking()
                .Where(x => x.TenantId == tenantId)
                .ToList();
        }

        public Employee? GetById(int id, int tenantId)
        {
            return _context.Employees.FirstOrDefault(x =>
                x.Id == id &&
                x.TenantId == tenantId);
        }

        public List<Employee> SearchEmployees(int tenantId, SearchEmployeeDto dto)
        {
            // I don't have IsActive or Department in my model for Employee
            return _context.Employees
                .AsNoTracking()
                .Where(x => x.TenantId == tenantId
                    && (x.Name.ToLower().Contains(dto.Name.ToLower()) || (x.Salary >= dto.MinimumSalary)))
                .ToList();
        }

        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Delete(Employee employee)
        {
            _context.Employees.Remove(employee);

            _context.SaveChanges();
        }
    }
}