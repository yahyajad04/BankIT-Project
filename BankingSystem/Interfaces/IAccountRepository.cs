using BankingSystem.DTO_s.Account;
using BankingSystem.Helpers;
using BankingSystem.Models;

namespace BankingSystem.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAll(QueryObject query, int ProfileId);
        Task<Account> GetById(int Id);
        Task<Account> GetByNumber(int AccountNumber);
        Task<Account> CreateAccount(Account account);
        Task<Account> EditAccount(int accountId, EditAccountDTO accountdto);
        Task<Account> DeleteAccount(int accountId);
        Task<Account> CloseAccount(int accountId);
        Task<Account> FreezeAccount(int accountId);
        Task<Account> ReActivateAccount(int accountId);
    }
}
