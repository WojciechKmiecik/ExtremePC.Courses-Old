using System.Threading.Tasks;
using ExtremePC.Courses.Contracts;

namespace ExtremePC.Courses.WebJobs.Services
{
    public interface IRegisterStudentService
    {
        Task<SignupStudent> DeserializeMessage(string payload);
        Task<bool> RegisterStudent(SignupStudent signupStudent);
    }
}