using ShoppingAssistant.Data;
using ShoppingAssistant.Models;
using ShoppingAssistant.Repository.Interfaces;

namespace ShoppingAssistant.Repository.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DataContext _context;
        private readonly IRoleRepository _roleRepository;

        public UserRepository(
            DataContext context,
            IRoleRepository roleRepository) : base(context)
        {
            _context = context;
            _roleRepository = roleRepository;
        }

        public async Task<User> UpdateRole(long userID, string roleValue)
        {
            var user = await GetAsync(x => x.ID == userID);

            if (user is null)
            {
                throw new InvalidOperationException("User not found");
            }

            var role = await _roleRepository.GetAsync(x => x.Value == roleValue);
            if (role is null)
            {
                throw new InvalidOperationException("CustomRole not found");
            }

            user.CustomRole = role;
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
