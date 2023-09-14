using ShoppingAssistant.Data;
using ShoppingAssistant.Models;
using ShoppingAssistant.Repository.Interfaces;

namespace ShoppingAssistant.Repository.Implementations
{
    public class PackageRepository : GenericRepository<Package>, IPackageRepository
    {
        private readonly DataContext _context;
        private readonly IProductRepository _productRepository;

        public PackageRepository(DataContext context, IProductRepository productRepository) : base(context)
        {
            _context = context;
            _productRepository = productRepository;
        }
    }
}
