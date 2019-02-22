using System.Net;
using System.Threading.Tasks;
using ExtremePC.Courses.Models;
using ExtremePC.Courses.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExtremePC.Courses.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly ISignupService _signupService;
        private readonly ILogger<SignupController> _logger;

        public SignupController(ISignupService signupService, ILogger<SignupController> logger)
        {
            _signupService = signupService;
            _logger = logger;
        }


        // POST: api/Signup
        [HttpPost("{courseId:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Forbidden)] 
        public async Task<IActionResult> PostSignup([FromRoute] int courseId, [FromBody] Student student)
        {
            
            var result = await _signupService.SignupStudentToCourseAsync(courseId, student.FirstName, student.LastName, student.Age).ConfigureAwait(false);
            if (result)
            {
                return Ok();
            }
            else
            {
                return Forbid();
            }            
        }

        // POST: api/SignupAsync
        [HttpPost("async/{courseId:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PostSignupAsync([FromRoute] int courseId, [FromBody] Student student)
        {
            // should be validation here
            

            var result = await _signupService.SendSignupStudentMessageAsync(courseId, student.FirstName, student.LastName, student.Age).ConfigureAwait(false);
            if (result)
            {
                return Ok();
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}