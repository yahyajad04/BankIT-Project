using System.ComponentModel.DataAnnotations;

namespace BankingSystem.DTO_s.AccountTypeDTO_s
{
    public class CreateAccountTypeDTO
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
