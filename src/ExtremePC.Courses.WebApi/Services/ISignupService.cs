using System.Threading.Tasks;

namespace ExtremePC.Courses.WebApi.Services
{
    public interface ISignupService
    {
        Task<bool> SendSignupStudentMessageAsync(int courseId, string firstName, string lastName, byte age);

        Task<bool> SignupStudentToCourseAsync(int courseId, string firstName, string lastName, byte age);
    }
}