using System.Text.Json.Serialization;

namespace BankingSystem.DTO_s.Account
{
    public class EditAccountDTO
    {

        public int? AccountTypeId { get; set; }
        public decimal? Balance { get; set; }

    }
}
