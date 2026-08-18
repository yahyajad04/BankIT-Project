namespace BankingSystem.Models
{
    public class EmployeeProfile
    {
        public int Id { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public string? NationalId { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Image { get; set; }
        public double? Salary { get; set; }
        public string LevelOfExperience { get; set; } = string.Empty;
    }
}
