using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Commonspace.Models;
using System.ComponentModel.DataAnnotations;

namespace Commonspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration!;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserSignUpModel model)
        {
            var user = new User { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                var token = GenerateJwtToken(user);
                return Ok(new { Token = token});
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserSignInModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                  return Unauthorized("User not found");
                }
                var token = GenerateJwtToken(user);
                return Ok(new {Token = token});
            }

            return Unauthorized();
        }

        private string GenerateJwtToken(User user)
        {
          if (user.Email == null)
          {
              throw new ArgumentException("User email cannot be null", nameof(user.Email));
          }

          string jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key cannot be null");
          string issuer = _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer cannot be null");
          string audience = _configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience cannot be null");

          var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
          var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

          var claims = new[]
          {
              new Claim(JwtRegisteredClaimNames.Sub, user.Email),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
          };

          var token = new JwtSecurityToken(
              issuer: issuer,
              audience: audience,
              claims: claims,
              expires: DateTime.Now.AddHours(3),
              signingCredentials: credentials
          );

          return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    #nullable disable
    public class UserSignUpModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class UserSignInModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
