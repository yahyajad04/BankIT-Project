using BankingSystem.DTO_s.CustomerProfileDTO_s;
using BankingSystem.Models;

namespace BankingSystem.Mappers
{
    public static class CustomerProfileMapper
    {
        public static CustomerProfileDTO toCustomerProfileDTO(this CustomerProfile customerProfile)
        {
            return new CustomerProfileDTO
            {
                AppUserId = customerProfile.AppUserId,
                AppUserDTO = customerProfile.AppUser?.toAppUserDTO(),
                Id = customerProfile.Id,
                NationalId = customerProfile.NationalId,
                Country = customerProfile.Country,
                City = customerProfile.City,
                PostalCode = customerProfile.PostalCode,
                Image = customerProfile.Image,
                Address = customerProfile.Address,
            };
        }
    }
}
