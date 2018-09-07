using System.Collections.Generic;
using System.Threading.Tasks;
using ExtremePC.Courses.Models.TableModels;

namespace ExtremePC.Courses.WebApi.Services
{
    public interface IStatisticsService
    {
        Task<IEnumerable<CourseCapacityStat>> GetCourseCapacityStatisticsAsync();
        Task<CourseDetailStat> GetCourseDetailsStatisticsAsync(int courseId);
    }
}