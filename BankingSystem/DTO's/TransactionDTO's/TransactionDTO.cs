using BankingSystem.DTO_s.Account;
using BankingSystem.DTO_s.Appuser;
using BankingSystem.Models;
using System.Transactions;

namespace BankingSystem.DTO_s.TransactionDTO_s
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int? FromAccountId { get; set; }
        public AccountDTO? FromAccount { get; set; }
        public int? ToAccountId { get; set; }
        public AccountDTO? ToAccount { get; set; }
        public decimal Amount { get; set; }
        public Transactiontype? Type { get; set; }
        public string PerformedById { get; set; } = null!;
        public AppUserDTO? PerformedBy { get; set; } = null!;
        public int? LoanId { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
