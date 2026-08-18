using System.ComponentModel.DataAnnotations;

namespace BankingSystem.DTO_s.Appuser
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? NationalId { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Name { get; set; }
    }
}
