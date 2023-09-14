using System.ComponentModel.DataAnnotations;

namespace ShoppingAssistant.Models
{
    public class CustomRole
    {
        [Key]
        public long ID { get; set; }
        public int ClearanceLevel { get; set; }
        public string Value { get; set; }
    }
}
