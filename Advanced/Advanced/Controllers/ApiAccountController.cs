using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Advanced.Controllers
{
    [ApiController]
    [Route("/api/account")]
    public class ApiAccountController : ControllerBase
    {
        private SignInManager<IdentityUser> signInManager;

        public ApiAccountController(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
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
    }

    public class Credentials
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
