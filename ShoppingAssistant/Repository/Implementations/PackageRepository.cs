using ShoppingAssistant.Data;
using ShoppingAssistant.Models;
using ShoppingAssistant.Repository.Interfaces;

namespace ShoppingAssistant.Repository.Implementations
{
    public class PackageRepository : GenericRepository<Package>, IPackageRepository
    {
        private readonly DataContext _context;

        public PackageRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
