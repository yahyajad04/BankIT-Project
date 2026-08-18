using System.ComponentModel.DataAnnotations;

namespace BankingSystem.DTO_s.Appuser
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
