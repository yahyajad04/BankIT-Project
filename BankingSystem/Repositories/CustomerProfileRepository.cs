using BankingSystem.Data;
using BankingSystem.DTO_s.CustomerProfileDTO_s;
using BankingSystem.Helpers;
using BankingSystem.Interfaces;
using BankingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace BankingSystem.Repositories
{
    public class CustomerProfileRepository : ICustomerProfileRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public CustomerProfileRepository(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<CustomerProfile> CreateProfile(CustomerProfile profile)
        {
            _context.CustomerProfiles.Add(profile);
            await _context.SaveChangesAsync();

            var customer = await _context.CustomerProfiles
                .FirstOrDefaultAsync(x => x.AppUserId.Equals(profile.AppUserId));
            if (customer == null)
                throw new KeyNotFoundException("This Profile Doesnt Exist");

            return customer;
        }

        public async Task<CustomerProfile> EditProfile(int Id, EditCustomerProfileDTO profiledto)
        {
            var profile = await _context.CustomerProfiles.Include(p => p.AppUser).FirstOrDefaultAsync(x => x.Id == Id);
            if (profile == null)
                throw new KeyNotFoundException("This Profile Doesnt Exist");

            profile.NationalId = profiledto.NationalId != null ? profiledto.NationalId : profile.NationalId;
            profile.Address = !string.IsNullOrEmpty(profiledto.Address) ? profiledto.Address : profile.Address ;
            profile.City = !string.IsNullOrEmpty(profiledto.City) ? profiledto.City : profile.City;
            profile.Country = !string.IsNullOrEmpty(profiledto.Country) ? profiledto.Country : profile.Country;
            profile.Image = !string.IsNullOrEmpty(profiledto.Image) ? profiledto.Image : profile.Image ;
            profile.PostalCode = profiledto.PostalCode != null ? profiledto.PostalCode : profile.PostalCode;

            await _context.SaveChangesAsync();

            return profile;
        }
        public async Task<CustomerProfile> EditCustomerProfile(int Id, EditProfileDTO profiledto)
        {
            var profile = await _context.CustomerProfiles.Include(p => p.AppUser).FirstOrDefaultAsync(x => x.Id == Id);

            if (profile == null)
                throw new KeyNotFoundException("This Profile Doesnt Exist");

            profile.Address = !string.IsNullOrEmpty(profiledto.Address) ? profiledto.Address : profile.Address;
            profile.Image = !string.IsNullOrEmpty(profiledto.Image) ? profiledto.Image : profile.Image;
            profile.PostalCode = profiledto.PostalCode != null ? profiledto.PostalCode : profile.PostalCode;

            if (!string.IsNullOrEmpty(profiledto.Email))
                await _userManager.SetEmailAsync(profile.AppUser, profiledto.Email);

            if (!string.IsNullOrEmpty(profiledto.UserName))
                await _userManager.SetUserNameAsync(profile.AppUser, profiledto.UserName);

            await _context.SaveChangesAsync();

            return profile;
        }
        public async Task<List<CustomerProfile>> GetAll(QueryObject query)
        {
            var profiles = _context.CustomerProfiles.Include(p => p.AppUser).AsQueryable();

            if (!query.Search.IsNullOrEmpty())
            {
                profiles = profiles.Where(p => 
                p.AppUser.Email.Contains(query.Search) ||
                p.AppUser.Name.Contains(query.Search) || 
                p.NationalId.ToString().Contains(query.Search)
                );
            }
            var skipnum = (query.PageNumber - 1) * query.PageSize;
            return await profiles.Skip(skipnum).Take(query.PageSize).ToListAsync();
             
        }
    }
}
