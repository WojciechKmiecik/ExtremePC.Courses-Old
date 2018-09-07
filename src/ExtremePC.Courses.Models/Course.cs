using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ExtremePC.Courses.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        public string Topic { get; set; }
        public ushort MaxCapacity { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
