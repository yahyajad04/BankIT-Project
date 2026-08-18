using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Models
{
    public class AccountType
    {
        [Key]
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool isUsable { get; set; } = false;
    }
}
