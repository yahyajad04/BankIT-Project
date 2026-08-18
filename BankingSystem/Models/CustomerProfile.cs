using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Models
{
    public class CustomerProfile
    {
        [Key]
        public int Id { get; set; }
        public string? AppUserId { get; set; } 
        public AppUser? AppUser { get; set; }
        public string? NationalId { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Image { get; set; }
        public List<Account> Accounts { get; set; } = new();
        public List<Loan> Loans { get; set; } = new List<Loan>();
    }
}
