using EmployeeManagement.DTOs;
using EmployeeManagement.Exceptions;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _service.GetAllEmployees();

            if (employees.Count == 0)
                return Ok(new List<Employee>());

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _service.GetEmployeeById(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployee(CreateEmployeeDto employee)
        {
            if (!_service.ValidateCreateEmployee(employee))
                return BadRequest();

            _service.AddEmployee(employee);

            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, UpdateEmployeeDto employee)
        {
            try
            {
                _service.UpdateEmployee(id, employee);
                return NoContent();
            }
            catch (EmployeeNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                _service.DeleteEmployee(id);
                return NoContent();
            }
            catch (EmployeeNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
