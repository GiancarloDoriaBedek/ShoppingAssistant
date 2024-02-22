namespace ShoppingAssistant.Models
{
    public class Product
    {
        public long ID { get; set; }
        public Store Store { get; set; }
        public long StoreID { get; set; }
        public ICollection<Package> Packages { get; set; }
        public Brand Brand { get; set; }
        public long BrandID { get; set; }
        public long ProductNativeID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string ImageURL { get; set; }
    }
}
