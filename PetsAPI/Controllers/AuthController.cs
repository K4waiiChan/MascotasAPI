using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetsAPI.Models;


namespace PetsAPI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost, Route("api/auth")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Login model invalid" });
            }
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(model.UserName);
                if (user == null)
                {
                    return BadRequest(new { message = "User doesn't exist" });
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                string tokenString = GenerateJSONWebToken(user);
                return Ok(new { token = tokenString });
            }
            return BadRequest(new { message = "Username or password invalid" });
        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var roles = _userManager.GetRolesAsync(user);
            roles.Wait();
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            foreach (string rol in roles.Result)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                 _configuration["Jwt:Issuer"],
                 claims,
                 expires: DateTime.Now.AddMinutes(60),
                 signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}