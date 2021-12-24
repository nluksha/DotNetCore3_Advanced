using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Advanced.Models
{
    public class Person
    {
        public long PersonId { get; set; }
        [Required(ErrorMessage = "Firstname is required")]
        [MinLength(3, ErrorMessage = "Firstname must be 3 or more chracters")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        [MinLength(3, ErrorMessage = "Surname must be 3 or more chracters")]
        public string Surname { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Department must be selected")]
        public long DepartmentId { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Location must be selected")]
        public long LocationId { get; set; }

        public Department Department { get; set; }
        public Location Location { get; set; }
    }
}
