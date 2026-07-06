using EmployeeManagement.Services;
using Microsoft.AspNetCore.Mvc;

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

            return Ok(employees);
        }
    }
}
