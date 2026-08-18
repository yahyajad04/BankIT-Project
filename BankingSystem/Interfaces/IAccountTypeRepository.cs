using BankingSystem.DTO_s.AccountTypeDTO_s;
using BankingSystem.Models;

namespace BankingSystem.Interfaces
{
    public interface IAccountTypeRepository
    {
        Task<List<AccountType>> GetAll();
        Task<AccountType> CreateType(AccountType type);
        Task<AccountType> EditType(int Id, EditAccountTypeDTO typedto);
        Task<AccountType> DeleteType(int Id);
    }
}
