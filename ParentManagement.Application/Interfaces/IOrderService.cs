using ParentManagement.Application.Models;
using ParentManagement.Domain.Entities;
using System.Threading.Tasks;

namespace ParentManagement.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Result> ProcessOrderAsync(Order order);
    }
}
