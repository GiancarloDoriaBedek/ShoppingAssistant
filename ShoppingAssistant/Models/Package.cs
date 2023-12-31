﻿namespace ShoppingAssistant.Models
{
    public class Package
    {
        public long ID { get; set; }
        public long ProductID { get; set; }
        public string Currency { get; set; }
        public decimal PackagePrice { get; set; }
        public decimal UnitPrice { get; set; } 
        public string Unit { get; set; }
        public string PackageUnit { get; set; }
        public double PackageQuantity { get; set; }

        // Set on special offers where daterange is known
        public DateTime? PriceStartDate { get; set; }
        public DateTime? PriceEndDate { get; set; }
        public DateTime PriceDate { get; set; }
    }
}
