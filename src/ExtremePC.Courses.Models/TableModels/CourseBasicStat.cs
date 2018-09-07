using System;
using System.Collections.Generic;
using System.Text;

namespace ExtremePC.Courses.Models.TableModels
{
    public abstract class CourseBasicStat
    {
        public int CourseId { get; set; }
        public string Topic { get; set; }
        public decimal AverageAge { get; set; }
        public byte MinAge { get; set; }
        public byte MaxAge { get; set; }
    }
}
