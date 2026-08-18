using BankingSystem.DTO_s.Account;
using BankingSystem.DTO_s.CustomerProfileDTO_s;
using BankingSystem.Models;
using Microsoft.Identity.Client;

namespace BankingSystem.DTO_s.LoanDTO_s
{
    public class LoanDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public AccountDTO? AccountDTO { get; set; }
        public decimal Amount { get; set; }
        public LoanType? Type { get; set; }
        public LoanStatus Status { get; set; } = LoanStatus.Pending;
        public decimal InterestRate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal MonthlyRepay { get; set; }
        public DateTime LastRepay { get; set; }
        public decimal RemaintoPay { get; set; }
    }
}
