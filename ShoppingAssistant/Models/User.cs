namespace ShoppingAssistant.Models
{
    public class User
    {
        public long ID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
#pragma warning disable CS8618
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
#pragma warning restore CS8618
    }
}
