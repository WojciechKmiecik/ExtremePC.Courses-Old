using System;
using System.Collections.Generic;
using System.Text;

namespace ExtremePC.Courses.Models.TableModels
{
    public class CourseCapacityStat : CourseBasicStat
    {
        public int MaxCapacity { get; set; }
        public int CurrentStudentsCount { get; set; }
    }
}
