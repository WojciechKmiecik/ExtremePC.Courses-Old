using ExtremePC.Courses.Contracts;
using ExtremePC.Courses.Database;
using ExtremePC.Courses.Models;
using Microsoft.Extensions.Logging;
using System;

using System.Linq;
using System.Threading.Tasks;

namespace ExtremePC.Courses.WebApi.Services
{
    public class SignupService : ISignupService
    {
        private readonly CoursesDbContext _context;
        private readonly ILogger<SignupService> _logger;
        private readonly ISignupStudentOverQueue _signupQueue;

        public SignupService(CoursesDbContext context, ILogger<SignupService> logger, ISignupStudentOverQueue signupQueue)
        {
            _context = context;
            _logger = logger;
            _signupQueue = signupQueue;
        }
        public async Task<bool> SendSignupStudentMessageAsync(int courseId, string firstName, string lastName, byte age)
        {
            try
            {
                _logger.LogInformation($"Attempt to sign-up student async {firstName} {lastName} to courseId {courseId}");
                var signupStudentTransportObject = new SignupStudent() { CourseId = courseId, FirstName = firstName, LastName = lastName, Age = age };
                var result = await _signupQueue.SignUpOverQueueAsync(signupStudentTransportObject).ConfigureAwait(false);
                _logger.LogInformation($"Attempt to sign-up async successful student {firstName} {lastName} to courseId {courseId}");
                return result;
            }
            catch (Exception) // exception should be changed ;) 
            {
                _logger.LogError($"Attempt to sign-up async successful student {firstName} {lastName} to courseId {courseId}");
                return await Task.FromResult(false);                
            }
        }

        public async Task<bool> SignupStudentToCourseAsync(int courseId, string firstName, string lastName, byte age)
        {
            _logger.LogInformation($"Attempt to sign-up student {firstName} {lastName} to courseId {courseId}");
            var course = await _context.Courses.FindAsync(courseId).ConfigureAwait(false);
            if (course == null)
            {
                _logger.LogError($"CourseId {courseId} do not exists");
                return false;
            }
            var currentStudentsList = (from cs in _context.CourseStudents
                                       where cs.CourseId == courseId
                                       select cs.StudentId).ToList();
            if (currentStudentsList.Count == course.MaxCapacity)
            {
                _logger.LogInformation($"CourseId {courseId} is full");
                return false;
            }

            var student = new Student()
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age
            };

            _context.Students.Add(student);


            _context.CourseStudents.Add(new CourseStudent()
            {
                CourseId = courseId,
                StudentId = student.StudentId
            });

            await _context.SaveChangesAsync().ConfigureAwait(false);
            _logger.LogInformation($"Attempt to sign-up successful student {firstName} {lastName} to courseId {courseId}");

            return true;
        }
        
    }
}
