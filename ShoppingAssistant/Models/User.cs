namespace ShoppingAssistant.Models
{
    public class User
    {
        public long ID { get; set; }
        public CustomRole CustomRole { get; set; }
        public long CustomRoleID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
