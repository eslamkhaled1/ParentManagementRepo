using ParentManagement.Application.Interfaces;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ParentManagement.Infrastructure.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly ConcurrentDictionary<string, decimal> _prices = new()
        {
            ["SKU1"] = 10.00m,
            ["SKU2"] = 20.00m,
            ["SKU3"] = 5.50m
        };

        public Task<decimal?> GetBasePriceAsync(string sku)
        {
            if (_prices.TryGetValue(sku, out var price)) return Task.FromResult<decimal?>(price);
            return Task.FromResult<decimal?>(null);
        }
    }
}
