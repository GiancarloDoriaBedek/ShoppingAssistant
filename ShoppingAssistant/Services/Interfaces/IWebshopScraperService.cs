using ShoppingAssistant.DTOs.ScraperDTOs;

namespace ShoppingAssistant.Services.Interfaces
{
    public interface IWebshopScraperService
    {
        Task<IEnumerable<ScrapeCategoriesResponseDTO>> ScrapeCategories(ScrapeCategoriesRequestDTO request);
        
        Task<IEnumerable<ScrapePagesResponseDTO>> ScrapePages(ScrapePagesRequestDTO request);

        Task<IEnumerable<ScrapeProductsResponseDTO>> ScrapeProducts(ScrapeProductsRequestDTO request);
    }
}
