using ShoppingAssistant.Models;

namespace ShoppingAssistant.Repository.Interfaces
{
    public interface IPackageRepository : IGenericRepository<Package>
    {
        Task<Dictionary<long, decimal>> GetLatestPriceForProducts(
            HashSet<long> productIDs,
            decimal? minPrice,
            decimal? maxPrice);
    }
}
