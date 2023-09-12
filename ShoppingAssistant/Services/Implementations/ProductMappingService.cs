using ShoppingAssistant.DTOs.ScraperDTOs;
using ShoppingAssistant.Models;
using ShoppingAssistant.Services.Interfaces;

namespace ShoppingAssistant.Services.Implementations
{
    public class ProductMappingService : IProductMappingService
    {
        public ProductMappingService()
        {
        }

        public Product MapToProduct(ScrapeProductsResponseDTO dto)
        {
            var product = new Product
            {
                Store = new Store
                {
                    Name = dto.StoreName
                },
                Packages = new List<Package>
                {
                    new Package
                    {
                        Currency = dto.ProductCurrency,
                        PackagePrice = dto.ProductPrice,
                        UnitPrice = dto.ProductPricePerUnitOfMeasurement,
                        Unit = dto.ProductUnitOfMeasurement,
                        PackageUnit = dto.ProductPackageUnitOfMeasurement,
                        PackageQuantity = dto.ProductPackageQuantity,
                        PriceStartDate = dto.ProductPriceStartDate,
                        PriceEndDate = dto.ProductPriceEndDate
                    }
                },
                Brand = new Brand
                {
                    Name = dto.ProductBrand
                },
                ProductNativeID = dto.ProductNativeID,
                Name = dto.ProductName,
                URL = dto.ProductURL,
                ImageURL = dto.ProductImageURL
            };

            return product;
        }

        public IEnumerable<Product> MapToProduct(IEnumerable<ScrapeProductsResponseDTO> dtos)
        {
            return dtos.Select(x => MapToProduct(x));
        }
    }
}
