using BankingSystem.Models;

namespace BankingSystem.Interfaces
{
    public interface ITokenService
    {
        Task<string> GetToken(AppUser user);
    }
}
