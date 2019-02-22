using ExtremePC.Courses.Common.Entities;
using ExtremePC.Courses.Common.Services;
using ExtremePC.Courses.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExtremePC.Courses.Database.Directory
{
    public class StatisticsTableRepository : IStatisticsTableRepository
    {
        private readonly IStorageAccountTableService _tableService;
        private const string CAPACITY_ROW_KEY = "CourseCapacityStat";
        private const string DETAIL_ROW_KEY = "CourseDetailStat";

        public StatisticsTableRepository(IStorageAccountTableService tableService)
        {
            _tableService = tableService;
        }
        public async Task<IEnumerable<CourseCapacityStat>> GetCourseCapacityStatistics(string rowKey = CAPACITY_ROW_KEY)
        {
            try
            {
                var courseCapacityStatList = await _tableService.GetFromTableStorageAsync<IEnumerable<CourseCapacityStat>>(rowKey).ConfigureAwait(false);
                return await Task.FromResult(courseCapacityStatList);
            }
            catch (Exception)
            {
                return await Task.FromResult<IEnumerable<CourseCapacityStat>>(null);
            }

        }
        public async Task<CourseDetailStat> GetCourseDetailStatistics(int CourseId, string rowKey = DETAIL_ROW_KEY)
        {
            try
            {
                var key = rowKey + CourseId;
                var courseDetailStat = await _tableService.GetFromTableStorageAsync<CourseDetailStat>(key).ConfigureAwait(false);
                return await Task.FromResult(courseDetailStat);
            }
            catch (Exception)
            {
                return await Task.FromResult<CourseDetailStat>(null);
            }
        }

        public async Task<bool> UpsertCourseCapacityStatistics(IEnumerable<CourseCapacityStat> stats, string rowKey = CAPACITY_ROW_KEY)
        {
            try
            {
                var entity = new StatisticsTableEntity<IEnumerable<CourseCapacityStat>>(_tableService.PARTITION_KEY, rowKey, stats);
                var courseCapacityStatList = await _tableService.UpsertToTableStorageAsync(entity).ConfigureAwait(false);
                return await Task.FromResult(courseCapacityStatList);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }

        }
        public async Task<bool> UpsertCourseDetailStatistics(CourseDetailStat stats, string rowKey = DETAIL_ROW_KEY)
        {
            try
            {
                var key = rowKey + stats.CourseId;
                var entity = new StatisticsTableEntity<CourseDetailStat>(_tableService.PARTITION_KEY, key, stats);
                var courseDetailStat = await _tableService.UpsertToTableStorageAsync(entity).ConfigureAwait(false);
                return await Task.FromResult(courseDetailStat);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }

        }
    }
}

