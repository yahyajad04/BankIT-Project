using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public long AccountNumber { get; set; }
        public int CustomerProfileId { get; set; }
        public CustomerProfile? Profile { get; set; }
        public int AccountTypeId { get; set; }
        public AccountType? AccountType { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
        public AccountStatus AccountStatus { get; set; } = AccountStatus.Active;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;
        public List<Transactions> SentTransactions { get; set; } = new List<Transactions>();
        public List<Transactions> RecievedTransactions { get; set; } = new List<Transactions>();
        public List<Loan> Loans { get; set; } = new List<Loan>();
        public string? AccountName { get; set; }
        public string? AccountImage { get; set; }
        public string? AccountDescription { get; set; }
    }
}
