using Microsoft.IdentityModel.Tokens;
using ShoppingAssistant.DTOs.ProductDTOs;
using ShoppingAssistant.Models;
using ShoppingAssistant.Repository.Interfaces;
using ShoppingAssistant.Services.Interfaces;

namespace ShoppingAssistant.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IPackageRepository _packageRepository;

        public ProductService(
            IProductRepository productRepository,
            IPackageRepository packageRepository)
        {
            _productRepository = productRepository;
            _packageRepository = packageRepository;
        }

        public async Task<IEnumerable<GetProductsResponseDTO>> GetProducts(GetProductsRequestDTO filter)
        {
            var products = await _productRepository.GetProducts(filter);

            var productIDs = products
                .Select(x => x.ID)
                .ToHashSet();

            var latestPricePerProduct = await _packageRepository.GetLatestPriceForProducts(
                productIDs,
                filter.MinPrice,
                filter.MaxPrice);

            products = products.Where(x => latestPricePerProduct.Keys.Contains(x.ID));

            var response = MapProductToProductResponse(products, latestPricePerProduct);

            return response;
        }

        private IEnumerable<GetProductsResponseDTO> MapProductToProductResponse(
            IEnumerable<Product> products,
            Dictionary<long, decimal> latestPricePerProduct)
            => products.Select(
                x => new GetProductsResponseDTO
                {
                    ID = x.ID,
                    StoreName = x.Store?.Name ?? string.Empty,
                    Price = latestPricePerProduct[x.ID],
                    BrandName = x.Brand?.Name ?? string.Empty,
                    ProductName = x.Name,
                    ProductURL = x.URL,
                    ProductImageURL = x.ImageURL,
                });
    }
}
