using Asp.Versioning;
using EmployeeManagement.DTOs;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers.V1
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _service;
        private readonly IInstanceIdService _instance1;
        private readonly IInstanceIdService _instance2;

        public EmployeeController(
            EmployeeService service, 
            IInstanceIdService instance1,
            IInstanceIdService instance2)
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
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto dto)
        {
            await _service.AddEmployee(dto);

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
