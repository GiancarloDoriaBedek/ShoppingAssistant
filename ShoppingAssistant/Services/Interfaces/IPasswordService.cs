namespace ShoppingAssistant.Services.Interfaces
{
    public interface IPasswordService
    {
        Task<(byte[] passwordHash, byte[] passwordSalt)> CreatePasswordHash(string password);
        Task<bool> VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
