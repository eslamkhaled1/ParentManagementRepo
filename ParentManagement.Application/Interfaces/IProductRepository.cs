using System.Threading.Tasks;

namespace ParentManagement.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<decimal?> GetBasePriceAsync(string sku);
    }
}
