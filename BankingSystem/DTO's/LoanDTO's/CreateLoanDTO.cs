using BankingSystem.DTO_s.CustomerProfileDTO_s;
using BankingSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankingSystem.DTO_s.LoanDTO_s
{
    public class CreateLoanDTO
    {
        [Required]
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public LoanType? Type { get; set; }
        public int MonthsRepay { get; set; }

    }
}
