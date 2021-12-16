using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Advanced.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        public DbSet<Person> People { get; set; }
        public DbSet<Department> Departmens { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
