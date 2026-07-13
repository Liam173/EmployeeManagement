using EmployeeManagement.Data;
using EmployeeManagement.DTOs;
using EmployeeManagement.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Services
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(
            ApplicationDbContext context,
            IOptions<JwtSettings> options)
        {
            _context = context;
            _jwtSettings = options.Value;
        }

        public string Login(LoginDto dto)
        {
            // 1. Find user
            var user = _context.Users
                .FirstOrDefault(x => x.Username == dto.Username);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials.");

            // 2. Verify password
            if (user.PasswordHash != dto.Password)
                throw new UnauthorizedAccessException("Invalid credentials.");

            // 3. Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // 4. Generate JWT
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: credentials);

            // 5. Return token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
