using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Models
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }
        public int? EmailVerificationCode { get; set; }
        public int? CustomerProfileId { get; set; }
        public CustomerProfile? CustomerProfile { get; set; }
        public int? EmployeeProfileId { get; set; }
        public EmployeeProfile? EmployeeProfile { get; set; }
    }
}
