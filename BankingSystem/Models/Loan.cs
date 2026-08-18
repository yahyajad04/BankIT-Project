using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Models
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account? Account { get; set; }
        public decimal Amount { get; set; }
        public LoanType? Type { get; set; }
        public LoanStatus Status { get; set; } = LoanStatus.Pending;
        public decimal InterestRate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal MonthlyRepay { get; set; }
        public DateTime LastRepay { get; set; }
        public decimal RemaintoPay { get; set; }
        public List<Transactions> Transactions { get; set; } = new List<Transactions>();
    }
}
