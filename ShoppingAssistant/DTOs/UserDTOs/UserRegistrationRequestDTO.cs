namespace ShoppingAssistant.DTOs.UserDTOs
{
    public class UserRegistrationRequestDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
