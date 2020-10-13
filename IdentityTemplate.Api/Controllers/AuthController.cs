
using IdentityTemplate.Api.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityTemplate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public AuthController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("register")] 
        public async Task<ActionResult> RegisterUser([FromBody] UserDTO model) 
        { 
            var user = new IdentityUser 
            { 
                UserName = model.Email, 
                Email = model.Email, 
                EmailConfirmed = true 
            }; 
            
            var result = await userManager.CreateAsync(user, model.Password); 
            
            
            if (!result.Succeeded) 
            { 
                return BadRequest(result.Errors); 
            } 
            
            await signInManager.SignInAsync(user, false); 
            
            return Ok(TokenGenerator(model)); }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserDTO model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok(TokenGenerator(model));
            }

            ModelState.AddModelError(string.Empty, "Incorrect email or password");
            return BadRequest(ModelState);
        }

        private object TokenGenerator(UserDTO model)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, model.Email, model.Password)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expirationTime = configuration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expirationTime));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["TokenConfiguration:Issuer"],
                audience: configuration["TokenConfiguration:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new TokenDTO()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "New Jwt Token"

            };
        }
    }
}
