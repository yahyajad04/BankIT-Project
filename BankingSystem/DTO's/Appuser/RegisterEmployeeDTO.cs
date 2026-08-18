using System.ComponentModel.DataAnnotations;

namespace BankingSystem.DTO_s.Appuser
{
    public class RegisterEmployeeDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Username { get; set; }
        public string? Name { get; set; }
        public double? Salary { get; set; }
        public string? LevelOfExperience { get; set; }
    }
}
