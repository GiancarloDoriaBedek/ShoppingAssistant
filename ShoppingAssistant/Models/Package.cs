namespace ShoppingAssistant.Models
{
    public class Package
    {
        public long ID { get; set; }
        public string Currency { get; set; }
        public decimal PackagePrice { get; set; }
        public decimal UnitPrice { get; set; } 
        public string Unit { get; set; }
        public string PackageUnit { get; set; }
        public double PackageQuantity { get; set; }
        public DateTime? PriceStartDate { get; set; }
        public DateTime? PriceEndDate { get; set; }
    }
}
