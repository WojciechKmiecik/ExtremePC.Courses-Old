using System.Threading.Tasks;

namespace ExtremePC.Courses.WebJobs.Services
{
    public interface IStatisticsCalculationService
    {
        Task<bool> CalculateCapacityStatistics();
        Task<bool> CalculateDetailsStatistics();
    }
}