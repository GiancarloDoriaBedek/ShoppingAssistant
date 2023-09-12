using ShoppingAssistant.Data;
using ShoppingAssistant.Models;
using ShoppingAssistant.Repository.Interfaces;

namespace ShoppingAssistant.Repository.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
