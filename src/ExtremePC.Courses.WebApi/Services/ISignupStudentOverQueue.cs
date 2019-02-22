using System.Threading.Tasks;
using ExtremePC.Courses.Contracts;

namespace ExtremePC.Courses.WebApi.Services
{
    public interface ISignupStudentOverQueue
    {
        Task<bool> SignUpOverQueueAsync(SignupStudent student);
    }
}