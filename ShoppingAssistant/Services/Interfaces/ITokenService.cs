using ShoppingAssistant.Models;

namespace ShoppingAssistant.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
