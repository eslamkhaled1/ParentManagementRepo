using ParentManagement.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ParentManagement.Infrastructure.Email
{
    public class FakeEmailSender : IEmailSender
    {
        private readonly ILogger<FakeEmailSender> _logger;

        public FakeEmailSender(ILogger<FakeEmailSender> logger)
        {
            _logger = logger;
        }

        public Task SendOrderConfirmationAsync(string to, string from, string subject, string body)
        {
            _logger.LogInformation("Sending email to {To} from {From}: {Subject} - {Body}", to, from, subject, body);
            return Task.CompletedTask;
        }
    }
}
