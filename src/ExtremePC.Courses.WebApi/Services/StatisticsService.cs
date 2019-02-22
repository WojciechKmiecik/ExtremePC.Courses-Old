using ExtremePC.Courses.Database.Directory;
using ExtremePC.Courses.Models.TableModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExtremePC.Courses.WebApi.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ILogger<StatisticsService> _logger;
        private readonly IStatisticsTableRepository _statisticsTable;

        public StatisticsService(ILogger<StatisticsService> logger, IStatisticsTableRepository statisticsTable)
        {
            _logger = logger;
            _statisticsTable = statisticsTable;
        }
        public async Task<IEnumerable<CourseCapacityStat>> GetCourseCapacityStatisticsAsync()
        {
            try
            {
                _logger.LogInformation("Attempt to read the CourseCapacityStat data from TableStorage");
                var result = await _statisticsTable.GetCourseCapacityStatistics().ConfigureAwait(false);
                _logger.LogInformation("Read attempt successful for CourseCapacityStat data from TableStorage");
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to read the CourseCapacityStat data from TableStorage, Reason : "+ e.Message);
                return await Task.FromResult<IEnumerable<CourseCapacityStat>>(null);
            }
        }
        public async Task<CourseDetailStat> GetCourseDetailsStatisticsAsync(int courseId)
        {
            try
            {
                _logger.LogInformation("Attempt to read the CourseDetailStat data from TableStorage");
                var result = await _statisticsTable.GetCourseDetailStatistics(courseId).ConfigureAwait(false);
                _logger.LogInformation("Read attempt successful for CourseDetailStat data from TableStorage");
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to read the CourseDetailStat data from TableStorage, Reason : " + e.Message);
                return await Task.FromResult<CourseDetailStat>(null);
            }
        }
    }
}
