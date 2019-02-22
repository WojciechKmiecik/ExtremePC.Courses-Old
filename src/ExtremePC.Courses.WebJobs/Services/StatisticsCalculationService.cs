using ExtremePC.Courses.Database;
using ExtremePC.Courses.Database.Directory;
using ExtremePC.Courses.Models.TableModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExtremePC.Courses.WebJobs.Services
{
    public class StatisticsCalculationService : IStatisticsCalculationService
    {
        private readonly ILogger<StatisticsCalculationService> _logger;
        private readonly CoursesDbContext _context;
        private readonly IStatisticsTableRepository _statisticsTable;

        public StatisticsCalculationService(ILogger<StatisticsCalculationService> logger, CoursesDbContext context, IStatisticsTableRepository statisticsTable)
        {
            _logger = logger;
            _context = context;
            _statisticsTable = statisticsTable;
        }

        public async Task<bool> CalculateCapacityStatistics()
        {
            try
            {

                List<CourseCapacityStat> courseCapacityStats = new List<CourseCapacityStat>();
                var coursesList = await (from cs in _context.CourseStudents
                                         group cs by cs.CourseId into csg
                                         join c in _context.Courses on csg.FirstOrDefault().CourseId equals c.CourseId
                                         select new
                                         {
                                             CourseId = c.CourseId,
                                             Topic = c.Topic,
                                             MaxCapacity = c.MaxCapacity,
                                             StudentsIdList = csg.Select(x => x.StudentId).ToList()
                                         }).ToListAsync().ConfigureAwait(false);
                foreach (var course in coursesList)
                {
                    var studentAgeList = await _context.Students.Where(x => course.StudentsIdList.Contains(x.StudentId)).Select(x => x.Age).ToListAsync();
                    var ageValues = CalculateMinMaxAvg(studentAgeList);

                    var courseCapacityStat = new CourseCapacityStat()
                    {
                        CourseId = course.CourseId,
                        Topic = course.Topic,
                        MaxCapacity = course.MaxCapacity,
                        CurrentStudentsCount = studentAgeList.Count(),
                        AverageAge = ageValues.Avg,
                        MaxAge = ageValues.Max,
                        MinAge = ageValues.Min
                    };
                    courseCapacityStats.Add(courseCapacityStat);
                }
                // save to table storage
                var result = await _statisticsTable.UpsertCourseCapacityStatistics(courseCapacityStats).ConfigureAwait(false);

                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError("Unsuccesful Calculation Of CourseCapacityStat, Reason : " + e.Message);
                return await Task.FromResult(false);
            }

        }

        public async Task<bool> CalculateDetailsStatistics()
        {
            try
            {
                List<CourseDetailStat> courseDetailStats = await (from cs in _context.CourseStudents
                                                                 group cs by cs.CourseId into csg
                                                                 join c in _context.Courses on csg.First().CourseId equals c.CourseId
                                                                 join t in _context.Teachers on c.TeacherId equals t.TeacherId

                                                                 select new CourseDetailStat()
                                                                 {
                                                                     CourseId = c.CourseId,
                                                                     Topic = c.Topic,
                                                                     TeacherId = t.TeacherId,
                                                                     TeacherName = t.FirstName + " " + t.LastName
                                                                     // not suported in EFCore :-/ 
                                                                     //StudentsList = _context.Students.Where(s => csg.Select(x => x.StudentId).ToList().Contains(s.StudentId)).ToList() 
                                                                 }).ToListAsync().ConfigureAwait(false);


                foreach (var course in courseDetailStats)
                {
                    course.StudentsList = await (from cs in _context.CourseStudents
                                                 join s in _context.Students on cs.StudentId equals s.StudentId
                                                 where cs.CourseId == course.CourseId
                                                 select s).ToListAsync().ConfigureAwait(false);

                    var studentAgeList = course.StudentsList.Select(x => x.Age).ToList();
                    var ageValues = CalculateMinMaxAvg(studentAgeList);
                    course.AverageAge = ageValues.Avg;
                    course.MaxAge = ageValues.Max;
                    course.MinAge = ageValues.Min;

                    var result = await _statisticsTable.UpsertCourseDetailStatistics(course);
                    if (!result)
                    {
                        return await Task.FromResult(false);
                    }
                }


                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                _logger.LogError("Unsuccesful Calculation Of CourseDetailStat, Reason : " + e.Message);
                return await Task.FromResult(false);
            }
        }
        private MinMaxAvg CalculateMinMaxAvg(List<byte> Ages)
        {
            var ret = new MinMaxAvg();
            foreach (var age in Ages)
            {
                if (age > ret.Max)
                {
                    ret.Max = age;
                }
                if (age < ret.Min)
                {
                    ret.Min = age;
                }
                ret.Avg = ret.Avg + age;
            }
            ret.Avg = ret.Avg / Ages.Count;
            return ret;
        }
        internal class MinMaxAvg
        {
            public byte Min = byte.MaxValue;
            public byte Max;
            public decimal Avg;
        }
    }

}
