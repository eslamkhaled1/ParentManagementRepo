using System.Threading.Tasks;

namespace ParentManagement.Application.Interfaces
{
    public interface IInventoryService
    {
        Task<int> GetStockAsync(string sku);
        Task<bool> ReduceStockAsync(string sku, int quantity);
    }
}
