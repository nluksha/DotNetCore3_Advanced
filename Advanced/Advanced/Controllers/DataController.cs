using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Advanced.Models;

namespace Advanced.Controllers
{
    [ApiController]
    [Route("/api/people")]
    [Authorize(AuthenticationSchemes = "Identity.Application, Bearer")]
    public class DataController : ControllerBase
    {
        private DataContext context;

        public DataController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Person> GetAll()
        {
            var people = context.People.Include(p => p.Department).Include(p => p.Location);

            foreach (var p in people)
            {
                p.Department.People = null;
                p.Location.People = null;
            }

            return people;
        }

        [HttpGet("{id}")]
        public async Task<Person> GetDetails(long id)
        {
            var p = await context.People.Include(p => p.Department).Include(p => p.Location).FirstAsync(p => p.PersonId == id);
            p.Department.People = null;
            p.Location.People = null;

            return p;
        }

        [HttpPost]
        public async Task Save([FromBody]Person p)
        {
            await context.People.AddAsync(p);
            await context.SaveChangesAsync();
        }

        [HttpPut]
        public async Task Update([FromBody] Person p)
        {
            context.Update(p);
            await context.SaveChangesAsync();
        }

        [HttpDelete]
        public async Task Delete(long id)
        {
            context.People.Remove(new Person { PersonId = id });
            await context.SaveChangesAsync();
        }

        [HttpGet("/api/locations")]
        public IAsyncEnumerable<Location> GetLocations() => context.Locations;

        [HttpGet("/api/departments")]
        public IAsyncEnumerable<Department> GetDepartments() => context.Departmens;
    }
}
