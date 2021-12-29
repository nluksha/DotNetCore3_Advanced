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
    public class ListModel : AdminPageModel
    {
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public IEnumerable<IdentityRole> Roles { get; set; }

        public ListModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void OnGet()
        {
            Roles = roleManager.Roles;
        }

        public async Task<string> GetMembersString(string role)
        {
            var users = await userManager.GetUsersInRoleAsync(role);

            string result = users.Count() == 0 ? "No menbers" : string.Join(", ", users.Take(3).Select(u => u.UserName).ToArray());

            return users.Count() > 3 ? $"{result}, (plus others)" : result;
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            await roleManager.DeleteAsync(role);

            return RedirectToPage();
        }
    }
}
