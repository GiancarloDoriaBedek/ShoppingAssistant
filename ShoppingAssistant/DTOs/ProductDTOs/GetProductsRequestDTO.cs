namespace ShoppingAssistant.DTOs.ProductDTOs
{
    public class GetProductsRequestDTO
    {
        public HashSet<long>? IDs { get; set; }
        public string? Name { get; set; }
        public string? Store { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
