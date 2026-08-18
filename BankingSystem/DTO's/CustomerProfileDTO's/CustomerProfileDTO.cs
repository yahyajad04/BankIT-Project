using BankingSystem.DTO_s.Appuser;
using BankingSystem.Models;

namespace BankingSystem.DTO_s.CustomerProfileDTO_s
{
    public class CustomerProfileDTO
    {
        public int Id { get; set; }
        public string? AppUserId { get; set; }
        public AppUserDTO? AppUserDTO { get; set; }
        public string? NationalId { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Image { get; set; }
    }
}
