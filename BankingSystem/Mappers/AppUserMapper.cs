using BankingSystem.DTO_s.Appuser;
using BankingSystem.Models;

namespace BankingSystem.Mappers
{
    public static class AppUserMapper
    {
        public static AppUserDTO toAppUserDTO(this AppUser appUser)
        {
            return new AppUserDTO
            {
                Email = appUser.Email,
                Name = appUser.Name,
                UserName = appUser.UserName,
            };
        }
    }
}
