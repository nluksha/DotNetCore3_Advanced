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
    public class EditorModel : AdminPageModel
    {
        public UserManager<IdentityUser> UserManager { get; set; }

        [BindProperty]
        [Required]
        public string Id { get; set; }

        [BindProperty]
        [Required]
        public string UserName { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public EditorModel(UserManager<IdentityUser> usermanager)
        {
            UserManager = usermanager;
        }

        public async Task OnGetAsync(string id)
        {
            IdentityUser user = await UserManager.FindByIdAsync(id);
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(Id);
                user.UserName = UserName;
                user.Email = Email;

                IdentityResult result = await UserManager.UpdateAsync(user);

                if(result.Succeeded && !String.IsNullOrEmpty(Password))
                {
                    await UserManager.RemovePasswordAsync(user);
                    result = await UserManager.AddPasswordAsync(user, Password);
                }

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
