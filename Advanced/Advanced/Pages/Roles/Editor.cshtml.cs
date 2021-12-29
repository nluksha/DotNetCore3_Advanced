using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Advanced.Pages.Roles
{
    public class EditorModel : AdminPageModel
    {
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public IdentityRole Role { get; set; }

        public EditorModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public Task<IList<IdentityUser>> Memders() => userManager.GetUsersInRoleAsync(Role.Name);

        public async Task<IEnumerable<IdentityUser>> NonMemders() => userManager.Users.ToList().Except(await Memders());

        public async Task OnGetAsync(string id)
        {
            Role = await roleManager.FindByIdAsync(id);
        }

        public async Task<IActionResult> OnPostAsync(string userId, string roleName)
        {
            Role = await roleManager.FindByNameAsync(roleName);
            var user = await userManager.FindByIdAsync(userId);
            IdentityResult result;

            if (await userManager.IsInRoleAsync(user, roleName))
            {
                result = await userManager.RemoveFromRoleAsync(user, roleName);
            } else
            {
                result = await userManager.AddToRoleAsync(user, roleName);
            }

            if (result.Succeeded)
            {
                return RedirectToPage(new { Id = Role.Id });
            } else
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }

                return Page();
            }
        }
    }
}
