using System.Threading.Tasks;

namespace ParentManagement.Application.Interfaces
{
    public interface ISchoolRepository
    {
        Task<string?> GetTierAsync(int schoolId);
    }
}
