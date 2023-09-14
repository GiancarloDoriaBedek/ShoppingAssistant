using ShoppingAssistant.Models;

namespace ShoppingAssistant.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> UpdateRole(long userID, string roleValue);
    }
}
