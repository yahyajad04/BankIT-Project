using BankingSystem.DTO_s.CustomerProfileDTO_s;
using BankingSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.DTO_s.Account
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public long AccountNumber { get; set; }
        public int CustomerProfileId { get; set; }
        public CustomerProfileDTO? Profile { get; set; }
        public int AccountTypeId { get; set; }
        public AccountType? AccountType { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
        public AccountStatus AccountStatus { get; set; } = AccountStatus.Active;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? AccountName { get; set; }
        public string? AccountImage { get; set; }
        public string? AccountDescription { get; set; }
    }
}
