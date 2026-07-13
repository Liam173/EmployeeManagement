using EmployeeManagement.DTOs;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthenticationService _service;

    public AuthController(AuthenticationService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var response = _service.Login(dto);

        return Ok(response);
    }
}