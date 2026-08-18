using BankingSystem.DTO_s.LoanDTO_s;
using BankingSystem.Models;

namespace BankingSystem.Mappers
{
    public static class LoanMapper
    {
        public static LoanDTO toLoanDTO(this Loan loan)
        {
            return new LoanDTO
            {
                Amount = loan.Amount,
                CreatedAt = loan.CreatedAt,
                AccountDTO = loan.Account.toAccountDTO(),
                AccountId = loan.AccountId,
                Id = loan.Id,
                InterestRate = loan.InterestRate,
                Type = loan.Type,
                LastRepay = loan.LastRepay,
                MonthlyRepay = loan.MonthlyRepay,
                Status = loan.Status,
                RemaintoPay = loan.RemaintoPay,
            };
        }
        public static LoanDTO toLoanFromEmployeeDTO(this Loan loan)
        {
            return new LoanDTO
            {
                Amount = loan.Amount,
                CreatedAt = loan.CreatedAt,
                AccountId = loan.AccountId,
                Id = loan.Id,
                InterestRate = loan.InterestRate,
                Type = loan.Type,
                LastRepay = loan.LastRepay,
                MonthlyRepay = loan.MonthlyRepay,
                Status = loan.Status,
                RemaintoPay = loan.RemaintoPay,
            };
        }
        public static Loan fromCreateLoanDTO(this CreateLoanDTO createLoanDTO) {
            return new Loan
            {
                AccountId = createLoanDTO.AccountId,
                Amount = createLoanDTO.Amount,
                Type = createLoanDTO.Type,
                LastRepay = DateTime.UtcNow.AddMonths(createLoanDTO.MonthsRepay)
            };
        }
    }
}
