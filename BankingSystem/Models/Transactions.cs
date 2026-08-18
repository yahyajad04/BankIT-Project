using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace BankingSystem.Models
{
    public class Transactions
    {
        [Key]
        public int Id { get; set; }
        public int? FromAccountId { get; set; }
        public Account? FromAccount { get; set; }
        public int? ToAccountId { get; set; }
        public Account? ToAccount { get; set; }
        public decimal Amount { get; set; }
        public Transactiontype? Type { get; set; }
        public string PerformedById { get; set; } = null!;
        public AppUser PerformedBy { get; set; } = null!;
        public int? LoanId { get; set; }
        public Loan? Loan { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
