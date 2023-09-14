using ShoppingAssistant.Data;
using ShoppingAssistant.Models;
using ShoppingAssistant.Repository.Interfaces;

namespace ShoppingAssistant.Repository.Implementations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext _context;
        private readonly IStoreRepository _storeRepository;
        private readonly IBrandRepository _brandRepository;

        public ProductRepository(
            DataContext context, 
            IStoreRepository storeRepository, 
            IBrandRepository brandRepository) : base(context)
        {
            _context = context;
            _storeRepository = storeRepository;
            _brandRepository = brandRepository;
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

            var existingStore = await _storeRepository.GetAsync(x => x.Name == product.Store.Name);
            if (existingStore is not null)
            {
                product.Store = existingStore;
            }

            var existingBrand = await _brandRepository.GetAsync(x => x.Name == product.Brand.Name);
            if (existingBrand is not null)
            {
                product.Brand = existingBrand;
            }

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
                if (!productFromDatabase.Packages?.Any(x => x.ID == newPackage.ID) ?? false)
                {
                    productFromDatabase.Packages ??= new List<Package>();
                    productFromDatabase.Packages.Add(newPackage);
                }
            }

            await SaveAsync();
        }
    }
}
