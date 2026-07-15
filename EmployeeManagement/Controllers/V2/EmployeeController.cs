using Asp.Versioning;
using EmployeeManagement.DTOs;
using EmployeeManagement.DTOs.V2;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers.V2
{
    [ApiController]
    [Authorize]
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _service;
        private readonly InstanceIdService _instance1;
        private readonly InstanceIdService _instance2;

        public EmployeeController(
            EmployeeService service, 
            InstanceIdService instance1,
            InstanceIdService instance2)
        {
            _service = service;
            _instance1 = instance1;
            _instance2 = instance2;
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
        public async Task<IActionResult> CreateEmployee(CreateEmployeeV2Dto dto)
        {
            var createDto = new CreateEmployeeDto
            {
                Name = dto.Name,
                Age = dto.Age,
                Salary = dto.Salary
            };

            await _service.AddEmployee(createDto);

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

        [AllowAnonymous]
        [HttpGet("InstanceId")]
        public IActionResult GetInstanceId()
        {
            return Ok(new
            {
                First = _instance1.InstanceId,
                Second = _instance2.InstanceId
            });
        }
    }
}
