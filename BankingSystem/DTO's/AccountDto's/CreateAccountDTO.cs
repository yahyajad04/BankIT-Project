using System.Text.Json.Serialization;

namespace BankingSystem.DTO_s.Account
{
    public class CreateAccountDTO
    {
        [JsonIgnore]
        public int CustomerProfileId { get; set; }
        public int AccountTypeId { get; set; }
        public decimal Balance { get; set; }
        public string? AccountName { get; set; }
        public string? AccountImage { get; set; }
        public string? AccountDescription { get; set; }
    }
}
