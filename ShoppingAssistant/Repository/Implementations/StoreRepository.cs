using ShoppingAssistant.Data;
using ShoppingAssistant.Models;
using ShoppingAssistant.Repository.Interfaces;

namespace ShoppingAssistant.Repository.Implementations
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
        private readonly DataContext _context;

        public StoreRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
