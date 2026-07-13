using EmployeeManagement.DTOs;
using EmployeeManagement.Exceptions;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }

        [HttpGet("GetAllEmployees")]
        [ProducesResponseType<List<EmployeeDto>>(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAllEmployees());
        }

        [HttpGet("GetEmployee/{id}")]
        [ProducesResponseType<EmployeeDto>(StatusCodes.Status200OK)]
        public IActionResult GetEmployeeById(int id)
        {
            return Ok(_service.GetEmployeeById(id));
        }

        [HttpPost("CreateEmployee")]
        public IActionResult CreateEmployee(CreateEmployeeDto dto)
        {
            _service.AddEmployee(dto);

            return Created();
        }

        [HttpPut("UpdateEmployee/{id}")]
        public IActionResult UpdateEmployee(int id, UpdateEmployeeDto dto)
        {
            _service.UpdateEmployee(id, dto);

            return NoContent();
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            _service.DeleteEmployee(id);

            return NoContent();
        }
    }
}
