using ShoppingAssistant.Services.Interfaces;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace ShoppingAssistant.Services.Implementations
{
    public class PasswordService : IPasswordService
    {
        public PasswordService()
        {

        }

        public async Task<(byte[] passwordHash, byte[] passwordSalt)> CreatePasswordHash(string password)
        {
            var hmac = new HMACSHA512();

            byte[] passwordSalt = hmac.Key;
            byte[] passwordHash = await Task.Run(() => hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));

            return (passwordHash, passwordSalt);
        }

        public async Task<bool> VerifyPasswordHash(
            string password,
            byte[] passwordHash,
            byte[] passwordSalt)
        {
            var hmac = new HMACSHA512(passwordSalt);
            var computedHash = await Task.Run(() => hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));

            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
