using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.EntityFrameworkCore;
using ShoppingAssistant.Data;
using ShoppingAssistant.Models;
using ShoppingAssistant.Repository.Interfaces;

namespace ShoppingAssistant.Repository.Implementations
{
    public class RoleRepository : GenericRepository<CustomRole>, IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CustomRole> GetLowestClearanceRole()
        {
            var roles = _context.CustomRoles
                .OrderBy(x => x.ClearanceLevel);

            if (!roles.Any())
            {
                throw new InvalidOperationException("No CustomRoles were found");
            }

            return await roles.FirstAsync();
        }

        public async Task<CustomRole> UpdateRole(CustomRole role)
        {
            _context.CustomRoles.Update(role);
            await _context.SaveChangesAsync();

            return role;
        }
    }
}
