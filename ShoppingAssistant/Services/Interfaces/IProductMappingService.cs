using ShoppingAssistant.DTOs.ScraperDTOs;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.Services.Interfaces
{
    public interface IProductMappingService
    {
        Product MapToProduct(ScrapeProductsResponseDTO dto);
        IEnumerable<Product> MapToProduct(IEnumerable<ScrapeProductsResponseDTO> dtos);
    }
}
