using BankingSystem.DTO_s.LoanDTO_s;
using BankingSystem.Models;

namespace BankingSystem.Interfaces
{
    public interface ILoanRepository
    {
        Task<List<Loan>> GetAll(int ProfileId, int AccountId);
        Task<List<Loan>> GetAllLoans(int ProfileId);
        Task<String[]> GetLoanTypes();
        Task<Loan> GetById(int Id);
        Task<Loan> CreateLoan(string UserId, Loan loan);
        Task<Loan> ApproveLoan(int Id);
        Task<Loan> DisApproveLoan(int Id);
        Task<Loan> MonthlyPayLoan(string UserId, int Id);
    }
}
