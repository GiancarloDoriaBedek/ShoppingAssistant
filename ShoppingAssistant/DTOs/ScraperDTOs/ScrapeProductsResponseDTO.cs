namespace ShoppingAssistant.DTOs.ScraperDTOs
{
    public class ScrapeProductsResponseDTO
    {
        public string StoreName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public long ProductNativeID  { get; set; }
        public string ProductBrand { get; set; } = string.Empty;
        public DateTime? ProductPriceStartDate { get; set; }
        public DateTime? ProductPriceEndDate { get; set; }
        public string ProductURL { get; set; } = string.Empty;
        public string ProductImageURL { get; set; } = string.Empty;
        public string ProductCurrency { get; set; } = string.Empty;
        public string ProductUnitOfMeasurement { get; set; } = string.Empty;
        public string ProductPackageUnitOfMeasurement { get; set; } = string.Empty;
        public double ProductPackageQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductPricePerUnitOfMeasurement { get; set; }
    }
}
