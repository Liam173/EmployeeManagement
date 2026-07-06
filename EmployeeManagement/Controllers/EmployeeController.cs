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
                return NotFound();

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
        public IActionResult CreateEmployee(Employee employee)
        {
            if (!_service.ValidateEmployee(employee))
                return BadRequest();

            _service.AddEmployee(employee);

            return Created();
        }

        [HttpPut]
        public IActionResult UpdateEmployee(int id, Employee employee)
        {
            if (_service.GetEmployeeById(id) == null)
                return NotFound();

           _service.UpdateEmployee(id, employee);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteEmployee(int id)
        {
            if (_service.GetEmployeeById(id) == null)
                return NotFound();

            // TODO: Delete the employee
            return NoContent();
        }
    }
}
