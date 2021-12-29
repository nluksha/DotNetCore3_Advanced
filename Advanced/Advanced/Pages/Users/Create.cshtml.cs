using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Advanced.Pages.Users
{
    public class CreateModel : AdminPageModel
    {
        public UserManager<IdentityUser> UserManager { get; set; }

        [BindProperty]
        [Required]
        public string UserName { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }


        public CreateModel(UserManager<IdentityUser> usermanager)
        {
            UserManager = usermanager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = UserName, Email = Email };
                IdentityResult result = await UserManager.CreateAsync(user, Password);

                if (result.Succeeded)
                {
                    return RedirectToPage("List");
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }

            return Page();
        }
    }
}
