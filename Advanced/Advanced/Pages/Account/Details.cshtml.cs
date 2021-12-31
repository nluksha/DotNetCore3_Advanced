using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Advanced.Pages.Account
{
    public class DetailsModel : PageModel
    {
        private UserManager<IdentityUser> userManager;

        public IdentityUser IdentityUser { get; set; }

        public DetailsModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                IdentityUser = await userManager.FindByNameAsync(User.Identity.Name);
            }
        }
    }
}
