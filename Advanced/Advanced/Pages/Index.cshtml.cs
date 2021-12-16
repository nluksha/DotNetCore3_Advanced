using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Advanced.Models;

namespace Advanced.Pages
{
    public class IndexModel : PageModel
    {
        private DataContext context;

        public IEnumerable<Person> People { get; set; }
        public IEnumerable<string> Cities { get; set; }

        [FromQuery]
        public string SelectedCity { get; set; }

        public IndexModel(DataContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            People = context.People.Include(p => p.Department).Include(p => p.Location);
            Cities = context.Locations.Select(l => l.City).Distinct();
        }

        public string GetClass(string city) => SelectedCity == city ? "bg-info" : "";
    }
}
