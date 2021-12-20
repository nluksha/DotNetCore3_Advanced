using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Advanced.Models;

namespace Advanced.Blazor
{
    public partial class Split
    {
        [Inject]
        public DataContext Contex { get; set; }

        public IEnumerable<string> Names => Contex.People.Select(p => p.Firstname);
    }
}
