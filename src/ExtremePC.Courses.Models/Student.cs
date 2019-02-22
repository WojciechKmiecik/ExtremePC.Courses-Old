using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtremePC.Courses.Models
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte Age { get; set; }
        public ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
