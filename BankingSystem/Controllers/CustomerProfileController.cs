using BankingSystem.DTO_s.CustomerProfileDTO_s;
using BankingSystem.Extensions;
using BankingSystem.Helpers;
using BankingSystem.Interfaces;
using BankingSystem.Mappers;
using BankingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerProfileController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomerProfileRepository _customerProfileRepository;
        public CustomerProfileController(UserManager<AppUser> userManager, ICustomerProfileRepository customerProfileRepository)
        {
            _userManager = userManager;
            _customerProfileRepository = customerProfileRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetAllProfiles([FromQuery] QueryObject query)
        {
            var profiles = await _customerProfileRepository.GetAll(query);
            var profilesdto = profiles.Select(p => p.toCustomerProfileDTO()).ToList();

            return Ok(profilesdto);
        }

        [HttpGet("GetProfilebyEmail")]
        [Authorize]
        public async Task<IActionResult> GetCustomerProfileByEmail(string Email)
        {
            var user = await _userManager.Users.Include(p => p.CustomerProfile).FirstOrDefaultAsync(u => u.Email.Equals(Email));
            if (user == null)
                return NotFound(new
                {
                    message = "User Doesnt Exist"
                });

            return Ok(user.CustomerProfile.toCustomerProfileDTO());
        }

        [HttpGet("GetProfile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.GetUserId();
            var user = await _userManager.Users.Include(p => p.CustomerProfile).FirstOrDefaultAsync(u => u.Id.Equals(userId));
            if (user == null)
                return NotFound(new
                {
                    message = "User Doesnt Exist"
                });

            if(user.CustomerProfile == null)
                return BadRequest(new
                {
                    message = "You are not a Customer"
                });

            return Ok(user.CustomerProfile.toCustomerProfileDTO());
        }

        [HttpPut("EditProfile")]
        [Authorize]
        public async Task<IActionResult> EditCustomerProfile(string email, EditCustomerProfileDTO profiledto)
        {
            try
            {
                var user = await _userManager.Users.Include(u => u.CustomerProfile).FirstOrDefaultAsync(e => e.Email == email);
                if (user == null)
                    return NotFound("A User with this email Doesnt Exist");

                var profile = await _customerProfileRepository.EditProfile(user.CustomerProfile.Id, profiledto);

                if (profile == null)
                    return NotFound("This Profile Doesnt Exist");

                return Ok(profile.toCustomerProfileDTO());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch(Exception ex) 
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }

        }

        [HttpPut("Edit")]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileDTO profiledto)
        {
            try
            {
                var userId = User.GetUserId();
                var user = await _userManager.Users.Include(u => u.CustomerProfile).FirstOrDefaultAsync(e => e.Id.Equals(userId));
               
                if (user == null)
                    return NotFound(new
                    {
                        message = "User Doesnt Exist"
                    });
                if (user.CustomerProfile == null)
                    return BadRequest(new
                    {
                        message = "You should be a Customer"
                    });

                var profile = await _customerProfileRepository.EditCustomerProfile(user.CustomerProfile.Id, profiledto);

                if (profile == null)
                    return NotFound(new
                    {
                        message = "This Profile Doesnt Exist"
                    });

                return Ok(profile.toCustomerProfileDTO());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }

        }
    }
}
