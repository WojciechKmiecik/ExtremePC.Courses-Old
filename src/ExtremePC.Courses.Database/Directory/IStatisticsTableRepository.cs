using System.Collections.Generic;
using System.Threading.Tasks;
using ExtremePC.Courses.Models.TableModels;

namespace ExtremePC.Courses.Database.Directory
{
    public interface IStatisticsTableRepository
    {
        Task<IEnumerable<CourseCapacityStat>> GetCourseCapacityStatistics(string rowKey = "CourseCapacityStat");
        Task<CourseDetailStat> GetCourseDetailStatistics(int CourseId, string rowKey = "CourseDetailStat");
        Task<bool> UpsertCourseCapacityStatistics(IEnumerable<CourseCapacityStat> stats, string rowKey = "CourseCapacityStat");
        Task<bool> UpsertCourseDetailStatistics(CourseDetailStat stats, string rowKey = "CourseDetailStat");
    }
}