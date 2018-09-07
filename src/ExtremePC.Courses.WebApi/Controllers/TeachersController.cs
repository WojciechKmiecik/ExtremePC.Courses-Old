using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExtremePC.Courses.Models;
using ExtremePC.Courses.Database;

namespace ExtremePC.Courses.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly CoursesDbContext _context;

        public TeachersController(CoursesDbContext context)
        {
            _context = context;
        }

        // GET: api/Teachers
        [HttpGet]
        public IEnumerable<Teacher> GetTeachers()
        {
            return _context.Teachers;
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacher = await _context.Teachers.FindAsync(id).ConfigureAwait(false);

            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        // PUT: api/Teachers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher([FromRoute] int id, [FromBody] Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacher.TeacherId)
            {
                return BadRequest();
            }

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Teachers
        [HttpPost]
        public async Task<IActionResult> PostTeacher([FromBody] Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetTeacher", new { id = teacher.TeacherId }, teacher);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacher = await _context.Teachers.FindAsync(id).ConfigureAwait(false);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return Ok(teacher);
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.TeacherId == id);
        }
    }
}