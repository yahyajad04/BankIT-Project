using BankingSystem.DTO_s.AccountTypeDTO_s;
using BankingSystem.Models;

namespace BankingSystem.Mappers
{
    public static class AccountTypeMapper
    {
        public static AccountTypeDTO toAccountTypeDTO(this AccountType account)
        {
            return new AccountTypeDTO
            {
                Id = account.Id,
                Description = account.Description,
                isUsable = account.isUsable,
                Name = account.Name,
            };
        }

        public static AccountType fromCreateAccountTypeDTO(this CreateAccountTypeDTO createAccountTypeDTO)
        {
            return new AccountType
            {
                Name = createAccountTypeDTO.Name,
                Description = createAccountTypeDTO.Description,
            };
        }
    }
}
