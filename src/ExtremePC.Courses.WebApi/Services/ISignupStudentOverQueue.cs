using System.Threading.Tasks;
using ExtremePC.Courses.Contracts;
using ExtremePC.Courses.Models.ConfigOptions;

namespace ExtremePC.Courses.WebApi.Services
{
    public interface ISignupStudentOverQueue
    {
        Task<bool> SignUpOverQueueAsync(SignupStudent student);
    }
}