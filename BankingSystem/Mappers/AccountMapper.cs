using BankingSystem.DTO_s.Account;
using BankingSystem.Models;

namespace BankingSystem.Mappers
{
    public static class AccountMapper
    {
        public static AccountDTO toAccountDTO(this Account account)
        {
            return new AccountDTO
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                AccountStatus = account.AccountStatus,
                AccountType = account.AccountType,
                AccountTypeId = account.AccountTypeId,
                Balance = account.Balance,
                CreatedAt = account.CreatedAt,
                CustomerProfileId = account.CustomerProfileId,
                Profile = account.Profile?.toCustomerProfileDTO(),
                AccountName = account.AccountName,
                AccountDescription = account.AccountDescription,
                AccountImage = account.AccountImage,
            };
        }

        public static Account fromCreateAccountDTO(this CreateAccountDTO createAccountDTO) {
            return new Account { 
                AccountTypeId = createAccountDTO.AccountTypeId,
                Balance = createAccountDTO.Balance,
                CustomerProfileId = createAccountDTO.CustomerProfileId,
                AccountName = createAccountDTO.AccountName,
                AccountDescription = createAccountDTO.AccountDescription,
                AccountImage = createAccountDTO.AccountImage,
            };
        }
    }
}
