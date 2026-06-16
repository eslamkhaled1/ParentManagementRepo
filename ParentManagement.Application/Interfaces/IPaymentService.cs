using System.Threading.Tasks;

namespace ParentManagement.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ChargeAsync(decimal amount, string email);
    }
}
