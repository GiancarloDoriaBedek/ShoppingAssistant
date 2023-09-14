using ShoppingAssistant.Models;

namespace ShoppingAssistant.Repository.Interfaces
{
    public interface IRoleRepository : IGenericRepository<CustomRole>
    {
        Task<CustomRole> GetLowestClearanceRole();
        Task<CustomRole> UpdateRole(CustomRole role);
    }
}
