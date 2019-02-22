using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtremePC.Courses.Models
{
    public class Teacher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
