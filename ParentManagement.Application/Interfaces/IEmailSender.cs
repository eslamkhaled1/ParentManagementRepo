using System.Threading.Tasks;

namespace ParentManagement.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendOrderConfirmationAsync(string to, string from, string subject, string body);
    }
}
