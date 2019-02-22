using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ExtremePC.Courses.Models.TableModels;
using ExtremePC.Courses.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExtremePC.Courses.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;
        private readonly ILogger<StatisticsController> _logger;

        public StatisticsController(IStatisticsService statisticsService, ILogger<StatisticsController> logger)
        {
            _statisticsService = statisticsService;
            _logger = logger;
        }


        // Get: api/Statistics
        [HttpGet()]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CourseCapacityStat>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<CourseCapacityStat>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetStatistics()
        {

            var result = await _statisticsService.GetCourseCapacityStatisticsAsync().ConfigureAwait(false);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // Get: api/Statistics
        [HttpGet("{courseId:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CourseDetailStat), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CourseDetailStat), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetDetailedStatistics([FromRoute] int courseId)
        {

            var result = await _statisticsService.GetCourseDetailsStatisticsAsync(courseId).ConfigureAwait(false);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


    }
}