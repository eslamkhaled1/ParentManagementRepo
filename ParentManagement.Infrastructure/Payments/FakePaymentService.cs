using ParentManagement.Application.Interfaces;
using System.Threading.Tasks;

namespace ParentManagement.Infrastructure.Payments
{
    public class FakePaymentService : IPaymentService
    {
        public Task<bool> ChargeAsync(decimal amount, string email)
        {
            // Fake payment always succeeds for demo. In real implementation call payment gateway.
            return Task.FromResult(true);
        }
    }
}

