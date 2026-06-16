using ParentManagement.Application.Interfaces;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ParentManagement.Infrastructure.Repositories
{
    public class InMemorySchoolRepository : ISchoolRepository
    {
        private readonly ConcurrentDictionary<int, string> _tiers = new()
        {
            [1] = "GOLD",
            [2] = "SILVER",
            [3] = "BRONZE"
        };

        public Task<string?> GetTierAsync(int schoolId)
        {
            _tiers.TryGetValue(schoolId, out var tier);
            return Task.FromResult<string?>(tier);
        }
    }
}
