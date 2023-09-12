namespace ShoppingAssistant.DTOs.ScraperDTOs
{
    public class ScrapePagesRequestDTO
    {
        public string StoreName { get; set; } = string.Empty;
        public string CategoryURL { get; set; } = string.Empty;
    }
}
