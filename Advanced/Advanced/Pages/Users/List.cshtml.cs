using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Advanced.Pages.Users
{
    public class ListModel : AdminPageModel
    {
        public UserManager<IdentityUser> UserManager { get; set; }
        public IEnumerable<IdentityUser> Users { get; set; }

        public ListModel(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }

        public void OnGet()
        {
            Users = UserManager.Users;
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                await UserManager.DeleteAsync(user);
            }

            return RedirectToPage();
        }
    }
}
