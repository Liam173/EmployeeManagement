using EmployeeManagement.Data;
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

        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee? GetById(int id)
        {
            return _context.Employees.Find(id);
        }

        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void Update(int id, Employee employee)
        {
            var existingEmployee = _context.Employees.Find(id);

            if (existingEmployee == null)
                throw new EmployeeNotFoundException(id);

            existingEmployee.Name = employee.Name;
            existingEmployee.Age = employee.Age;
            existingEmployee.Salary = employee.Salary;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var existingEmployee = _context.Employees.Find(id);

            if (existingEmployee == null)
                throw new EmployeeNotFoundException(id);

            _context.Employees.Remove(existingEmployee);
            _context.SaveChanges();
        }
    }
}