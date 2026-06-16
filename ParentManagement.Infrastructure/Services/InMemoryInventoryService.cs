using ParentManagement.Application.Interfaces;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ParentManagement.Infrastructure.Services
{
    public class InMemoryInventoryService : IInventoryService
    {
        private readonly ConcurrentDictionary<string, int> _stock = new()
        {
            ["SKU1"] = 100,
            ["SKU2"] = 5,
            ["SKU3"] = 0
        };

        public Task<int> GetStockAsync(string sku)
        {
            _stock.TryGetValue(sku, out var qty);
            return Task.FromResult(qty);
        }

        public Task<bool> ReduceStockAsync(string sku, int quantity)
        {
            // only reduce if enough stock exists
            if (!_stock.TryGetValue(sku, out var current) || current < quantity) return Task.FromResult(false);
            _stock[sku] = current - quantity;
            return Task.FromResult(true);
        }
    }
}
