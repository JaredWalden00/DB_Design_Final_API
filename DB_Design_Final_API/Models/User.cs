using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DB_Design_Final_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // "Customer" or "Employee"
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly DataContext _context;

        public AuthController(IConfiguration config, DataContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role", userInfo.Role)
            };

            var token = new JwtSecurityToken(
              issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Issuer"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User AuthenticateUser(User login)
        {
            // This method should validate the user credentials with the database
            var user = _context.Users.FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);
            return user;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Role))
            {
                return BadRequest("Invalid user data.");
            }

            // Check if the user already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (existingUser != null)
            {
                return Conflict("Username already exists.");
            }

            // Create the user
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Depending on the role, create the corresponding customer or employee record
            if (user.Role == "Customer")
            {
                var customer = new Customer
                {
                    Cust_ID = user.Id,
                    Name = user.Username, // For simplicity, assuming Username as Name, you can modify as needed
                    Address = "", // Default empty, can be updated later
                    Balance = 0, // Default balance
                    CCInfos = new List<CCInfo>() // Default empty, can be updated later
                };
                _context.Customers.Add(customer);
            }
            else if (user.Role == "Employee")
            {
                var employee = new Employee
                {
                    Empl_ID = user.Id,
                    Name = user.Username, // For simplicity, assuming Username as Name, you can modify as needed
                    Address = "", // Default empty, can be updated later
                    Salary = 0, // Default salary
                    Title = "" // Default title
                };
                _context.Employees.Add(employee);
            }

            await _context.SaveChangesAsync();
            return Ok("User registered successfully.");
        }
    }
}
