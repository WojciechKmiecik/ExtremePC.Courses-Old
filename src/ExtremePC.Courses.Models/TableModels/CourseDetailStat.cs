using System;
using System.Collections.Generic;
using System.Text;

namespace ExtremePC.Courses.Models.TableModels
{
    public class CourseDetailStat  : CourseBasicStat
    {
        public CourseDetailStat()
        {
            StudentsList = new List<Student>();
        }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }

        public IEnumerable<Student> StudentsList { get; set; }
    }
}
