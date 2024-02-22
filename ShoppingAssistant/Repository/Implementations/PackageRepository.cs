using Microsoft.EntityFrameworkCore;
using ShoppingAssistant.Data;
using ShoppingAssistant.Models;
using ShoppingAssistant.Repository.Interfaces;
using ShoppingAssistant.Services.Interfaces;

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

        public async Task<Dictionary<long, decimal>> GetLatestPriceForProducts(
            HashSet<long> productIDs, 
            decimal? minPrice,
            decimal? maxPrice)
        {
            //var latestPricesPerProductIDs = await _context.Packages
            //    .Where(x => productIDs.Contains(x.ProductID))
            //    .Select(x => new
            //    {
            //        x.ProductID,
            //        x.PackagePrice,
            //        x.PriceDate
            //    })
            //    .GroupBy(x => x.ProductID)
            //    .ToDictionaryAsync(
            //        x => x.Key,
            //        x => x.OrderByDescending(y => y.PriceDate)
            //        .Select(y => y.PackagePrice)
            //        .FirstOrDefault());

            var latestPricesPerProductIDs = await _context.Packages
                .Where(p => productIDs.Contains(p.ProductID))
                .GroupBy(p => p.ProductID)
                .Select(g => new
                {
                    ProductID = g.Key,
                    LatestPrice = g.OrderByDescending(p => p.PriceDate).Select(p => p.PackagePrice).FirstOrDefault()
                })
                .Where(x => 
                    (x.LatestPrice >= minPrice || !minPrice.HasValue) 
                    && (x.LatestPrice <= maxPrice || !maxPrice.HasValue))
                .ToDictionaryAsync(
                    x => x.ProductID,
                    x => x.LatestPrice);

            return latestPricesPerProductIDs;
        }
    }
}
