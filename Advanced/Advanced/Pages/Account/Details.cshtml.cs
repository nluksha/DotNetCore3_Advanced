using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Advanced.Pages.Account
{
    public class DetailsModel : PageModel
    {
        public string Cookie { get; set; }

        public void OnGet()
        {
            Cookie = Request.Cookies[".AspNetCore.Identity.Application"];
        }
    }
}