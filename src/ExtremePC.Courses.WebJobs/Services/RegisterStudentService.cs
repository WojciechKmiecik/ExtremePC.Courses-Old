using ExtremePC.Courses.Common.Services;
using ExtremePC.Courses.Contracts;
using ExtremePC.Courses.Database;
using ExtremePC.Courses.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtremePC.Courses.WebJobs.Services
{
    public class RegisterStudentService : IRegisterStudentService
    {
        private readonly CoursesDbContext _context;
        private readonly IStorageAccountMessageService _messageService;
        private readonly ILogger<RegisterStudentService> _logger;
        private readonly IMailingService _mailingService;

        public RegisterStudentService(CoursesDbContext dbContext, IStorageAccountMessageService messageService, ILogger<RegisterStudentService> logger, IMailingService mailingService)
        {
            _context = dbContext;
            _messageService = messageService;
            _logger = logger;
            _mailingService = mailingService;
        }
        public async Task<SignupStudent> DeserializeMessage(string payload)
        {
            try
            {
                var deserialized = JsonConvert.DeserializeObject<SignupStudent>(payload);
                _logger.LogInformation("Student deserialization successful");
                return await Task.FromResult(deserialized).ConfigureAwait(false);
            }
            catch (JsonException)
            {
                _logger.LogError("Student deserialization successful");
                return await Task.FromResult<SignupStudent>(null).ConfigureAwait(false);
            }
        }

        public async Task<bool> RegisterStudent(SignupStudent signupStudent)
        {
            _logger.LogInformation($"Attempt to sign-up student {signupStudent.FirstName} {signupStudent.LastName} to courseId {signupStudent.CourseId}");
            var course = await _context.Courses.FindAsync(signupStudent.CourseId).ConfigureAwait(false);
            if (course == null)
            {
                _logger.LogError($"CourseId {signupStudent.CourseId} do not exists");
                return await Task.FromResult(false).ConfigureAwait(false); ;
            }
            var currentStudentsList = (from cs in _context.CourseStudents
                                       where cs.CourseId == signupStudent.CourseId
                                       select cs.StudentId).ToList();
            if (currentStudentsList.Count == course.MaxCapacity)
            {
                _logger.LogInformation($"CourseId {signupStudent.CourseId} is full");
                await _mailingService.SendDenyEmail(); // with message that course is full
                return await Task.FromResult(false).ConfigureAwait(false);
            }

            var student = new Student()
            {
                FirstName = signupStudent.FirstName,
                LastName = signupStudent.LastName,
                Age = signupStudent.Age
            };

            _context.Students.Add(student);


            _context.CourseStudents.Add(new CourseStudent()
            {
                CourseId = signupStudent.CourseId,
                StudentId = student.StudentId
            });

            await _context.SaveChangesAsync().ConfigureAwait(false);
            _logger.LogInformation($"Sign-up successful for student {signupStudent.FirstName} {signupStudent.LastName} to courseId {signupStudent.CourseId}");
            await _mailingService.SendConfirmationEmail(); // with message that Student is accepted to course
            return await Task.FromResult(true).ConfigureAwait(false);
        }

    }
}

