using BankingSystem.DTO_s.Appuser;
using BankingSystem.Extensions;
using BankingSystem.Interfaces;
using BankingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Net.Http;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomerProfileRepository _customerProfileRepository;
        private readonly ITokenService _tokenService;
        private readonly IEmployeeProfileRepository _employeeProfileRepository;
        private readonly ILogger<AppUserController> _logger;
        private readonly HttpClient _httpClient;
        public AppUserController(UserManager<AppUser> userManager,
            ICustomerProfileRepository customerProfileRepository, ITokenService tokenService
            , IEmployeeProfileRepository employeeProfileRepository, ILogger<AppUserController> logger,
            HttpClient httpClient)
        {
            _userManager = userManager;
            _customerProfileRepository = customerProfileRepository;
            _tokenService = tokenService;
            _employeeProfileRepository = employeeProfileRepository;
            _logger = logger;
            _httpClient = httpClient;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterDTO registerdto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new AppUser
                {
                    Email = registerdto.Email,
                    UserName = registerdto.Username,
                    Name = registerdto.Name,
                };
                var profile = new CustomerProfile
                {
                    AppUserId = user.Id,
                    AppUser = user,
                    NationalId = registerdto.NationalId,
                    Country = registerdto.Country,
                    City = registerdto.City
                };

                var existUser = await _userManager.FindByEmailAsync(registerdto.Email);
                if (existUser != null)
                    return BadRequest("A User Already Exists with this Email");

                var createdUser = await _userManager.CreateAsync(user, registerdto.Password);
                if (createdUser.Succeeded)
                {
                    var role = await _userManager.AddToRoleAsync(user, "Customer");
                    if (role.Succeeded)
                    {
                        var customer = await _customerProfileRepository.CreateProfile(profile);
                        user.CustomerProfileId = customer.Id;
                        user.CustomerProfile = customer;
                        return Ok("User Created Successfully!");
                    }
                    else
                        return StatusCode(500, "User Not Added to Role");
                }
                else
                    return StatusCode(500, "User Creation Failed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin(RegisterDTO registerdto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new AppUser
                {
                    Email = registerdto.Email,
                    UserName = registerdto.Username,
                    Name = registerdto.Name,
                };

                var existUser = await _userManager.FindByEmailAsync(registerdto.Email);
                if (existUser != null)
                    return BadRequest("A User Already Exists with this Email");

                var createdUser = await _userManager.CreateAsync(user, registerdto.Password);
                if (createdUser.Succeeded)
                {
                    var role = await _userManager.AddToRoleAsync(user, "Admin");
                    if (role.Succeeded)
                    {
                        return Ok("User Created Successfully!");
                    }
                    else
                        return StatusCode(500, "Admin Not Added to Role");
                }
                else
                    return StatusCode(500, "User Creation Failed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("RegisterEmployee")]
        [Authorize(Roles ="Admin,Employee")]
        public async Task<IActionResult> RegisterEmployee(RegisterEmployeeDTO registerdto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var UserId = User.GetUserId();
                var logged_user = await _userManager.FindByIdAsync(UserId);

                _logger.LogInformation($"User {logged_user.UserName} ({logged_user.Email}) " +
                    $"is trying to Register a new Employee with Email: {registerdto.Email} ");
                var user = new AppUser
                {
                    Email = registerdto.Email,
                    UserName = registerdto.Username,
                    Name = registerdto.Name,
                };

                var profile = new EmployeeProfile
                {
                    AppUser = user,
                    AppUserId = user.Id,
                    Salary = registerdto.Salary,
                    LevelOfExperience = registerdto.LevelOfExperience ?? "Not Specified"
                };

                var existUser = await _userManager.FindByEmailAsync(registerdto.Email);
                if (existUser != null)
                {
                    _logger.LogError($"FAILED TO REGISTER EMPLOYEE: A User Already Exists with this Email");
                    return BadRequest("A User Already Exists with this Email");
                }

                var createdUser = await _userManager.CreateAsync(user, registerdto.Password);
                if (createdUser.Succeeded)
                {
                    var role = await _userManager.AddToRoleAsync(user, "Employee");
                    if (role.Succeeded)
                    {
                        var employee = await _employeeProfileRepository.CreateProfile(profile);
                        user.EmployeeProfile = employee;
                        user.EmployeeProfileId = employee.Id;

                        _logger.LogInformation($"User {logged_user.UserName} ({logged_user.Email}) " +
                            $"Registered a new Employee with Email: {registerdto.Email} ");

                        return Ok("User Created Successfully!");
                    }
                    else
                    {
                        _logger.LogError($"FAILED TO REGISTER EMPLOYEE: Employee Not Added to Role");
                        return StatusCode(500, "Employee Not Added to Role");
                    }
                }
                else
                {
                    _logger.LogError($"FAILED TO REGISTER EMPLOYEE");
                    return StatusCode(500, "User Creation Failed");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"FAILED TO REGISTER EMPLOYEE: {ex}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO logindto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = await _userManager.FindByEmailAsync(logindto.Email);
                if (user == null) return BadRequest(new
                {
                    message = "Wrong Email or Password"
                });

                _logger.LogInformation($"Trying to Login with Profile Email: {logindto.Email}");

                var pass_check = await _userManager.CheckPasswordAsync(user, logindto.Password);
                if (!pass_check)
                {
                    _logger.LogWarning($"Wrong Password Attempt on Profile with Email: {logindto.Email}");
                    return BadRequest(new
                    {
                        message = "Wrong Email or Password"
                    });
                }

                var token = await _tokenService.GetToken(user);

                if (token == null) return StatusCode(500, new
                {
                    message = "Token Error Occured"
                });

                var response = new GetLoginDTO
                {
                    Welcome = $"Welcome {user.Name}",
                    token = token
                };

                _logger.LogInformation($"Logged In with the Profile Email: {logindto.Email}");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"FAILED TO LOGIN: {ex}");
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost("GetRole")]
        [Authorize]
        public async Task<IActionResult> GetRole()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null) return BadRequest(new
            {
                message = "An Error Occured"
            });

            var role = await _userManager.GetRolesAsync(user);

            if (role == null) return BadRequest(new
            {
                message = "An Error Occured"
            });

            return Ok(role);
        }

        [HttpGet("Countries")]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _httpClient.GetFromJsonAsync<object>(
                "https://countries.dev/countries"
            );

            return Ok(countries);
        }
    }
}
