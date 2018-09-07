using System.Threading.Tasks;

namespace ExtremePC.Courses.WebJobs.Services
{
    public interface IMailingService
    {
        Task SendConfirmationEmail();
        Task SendDenyEmail();
    }
}