namespace ShoppingAssistant.DTOs.ScraperDTOs
{
    public class ScrapeProductsRequestDTO
    {
        public string StoreName { get; set; } = string.Empty;
        public string PageURL { get; set; } = string.Empty;
    }
}
