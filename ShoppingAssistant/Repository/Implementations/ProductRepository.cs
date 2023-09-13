using ShoppingAssistant.Data;
using ShoppingAssistant.Models;
using ShoppingAssistant.Repository.Interfaces;

namespace ShoppingAssistant.Repository.Implementations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            // TODO: implement smarter way to search by name
            var productsOfSimilarName = await GetAllAsync(x => x.Name.Contains(name));

            return productsOfSimilarName;
        }

        public async Task SaveProduct(Product product)
        {
            var productFromDatabase = await GetAsync(x => x.ProductNativeID == product.ProductNativeID);

            if (productFromDatabase is null) 
            {
                await CreateAsync(product);
                return;
            }
            
            await UpdateProduct(productFromDatabase, product);
        }


        private async Task UpdateProduct(Product productFromDatabase, Product product)
        {
            foreach (var newPackage in product.Packages)
            {
                if (!productFromDatabase.Packages.Any(x => x.ID == newPackage.ID))
                {
                    productFromDatabase.Packages.Add(newPackage);
                }
            }

            await SaveAsync();
        }

    }
}
