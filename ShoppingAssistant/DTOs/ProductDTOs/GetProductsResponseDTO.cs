namespace ShoppingAssistant.DTOs.ProductDTOs
{
    public class GetProductsResponseDTO
    {
        public long ID { get; set; }
        public string StoreName { get; set; }
        public decimal? Price { get; set; }
        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public string ProductURL { get; set; }
        public string ProductImageURL { get; set; }
    }
}
