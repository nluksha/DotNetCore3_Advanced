using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Advanced.Controllers
{
    [ApiController]
    [Route("/api/account")]
    public class ApiAccountController : ControllerBase
    {
        private SignInManager<IdentityUser> signInManager;
        private UserManager<IdentityUser> userManager;
        private IConfiguration configuration;

        public ApiAccountController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Credentials creds)
        {
            var result = await signInManager.PasswordSignInAsync(creds.UserName, creds.Password, false, false);

            if (result.Succeeded)
            {
                return Ok();
            }

            return Unauthorized();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return Ok();
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody]Credentials creds)
        {
            if (await CheckPassword(creds))
            {
                var handler = new JwtSecurityTokenHandler();
                byte[] secret = Encoding.ASCII.GetBytes(configuration["jwtSecret"]);

                var descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, creds.UserName)
                    }),
                    Expires = DateTime.UtcNow.AddHours(24),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = handler.CreateToken(descriptor);

                return Ok(new
                {
                    success = true,
                    token = handler.WriteToken(token)
                });
            }

            return Unauthorized();
        }

        private async Task<bool> CheckPassword(Credentials creds)
        {
            var user = await userManager.FindByNameAsync(creds.UserName);

            if (user != null)
            {
                foreach (var v in userManager.PasswordValidators)
                {
                    if ((await v.ValidateAsync(userManager, user, creds.Password)).Succeeded)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    public class Credentials
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
