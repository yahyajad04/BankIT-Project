using BankingSystem.DTO_s.Account;
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
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAccountRepository accountRepository, 
            UserManager<AppUser> userManager,
            ILogger<AccountController> logger)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAccounts([FromQuery] QueryObject query)
        {
            try
            {
                var UserId = User.GetUserId();
                var user = await _userManager.Users.Include(p => p.CustomerProfile)
                    .FirstOrDefaultAsync(u => u.Id.Equals(UserId));
                var roles = await _userManager.GetRolesAsync(user);

                var profileId = -1;

                if (roles[0].Equals("Customer"))
                    profileId = user.CustomerProfile.Id;

                if (user.CustomerProfile == null)
                    profileId = -1;

                var accounts = await _accountRepository.GetAll(query, profileId);
                var accountsdto = accounts.Select(a => a.toAccountDTO());

                return Ok(accountsdto);
            }
            catch(KeyNotFoundException ex)
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

        [HttpGet("{Id}")]
        [Authorize]
        public async Task<IActionResult> GetAccountById(int Id)
        {
            var account = await _accountRepository.GetById(Id);
            if (account == null)
                return NotFound(new
                {
                    message = "This Account Doesnt Exist"
                });
            return Ok(account.toAccountDTO());
        }

        [HttpGet("GetByNumber/{AccountNumber}")]
        [Authorize]
        public async Task<IActionResult> GetAccountByNumber(int AccountNumber)
        {
            var account = await _accountRepository.GetByNumber(AccountNumber);
            if (account == null)
                return NotFound(new
                {
                    message = "This Account Doesnt Exist"
                });
            return Ok(account.toAccountDTO());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAccount(CreateAccountDTO accountdto)
        {
            var userId = User.GetUserId();
            var user = await _userManager.Users.Include(c => c.CustomerProfile).FirstOrDefaultAsync(u => u.Id == userId);
            accountdto.CustomerProfileId = user.CustomerProfile.Id;

            _logger.LogInformation($"{user.UserName} with email {user.Email} is trying to Create an Account with balance: {accountdto.Balance}");
            var account = accountdto.fromCreateAccountDTO();

            var account_done = await _accountRepository.CreateAccount(account);
            if (account_done == null)
            {
                _logger.LogError($"{user.UserName} with email {user.Email} Tried to Create an Account with balance: {accountdto.Balance}, but it FAILED");
                return BadRequest(new
                {
                    message = "Creation Error Happened"
                });
            }

            _logger.LogInformation($"{user.UserName} with email {user.Email} Created an Account with balance: {accountdto.Balance}");
            return Ok(account_done.toAccountDTO());

        }

        [HttpPut("{Id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> EditAccount([FromRoute] int Id, EditAccountDTO accountdto)
        {
            try
            {
                var userId = User.GetUserId();
                var user = await _userManager.FindByIdAsync(userId);

                _logger.LogInformation($"{user.UserName} with email {user.Email} is " +
                    $"trying to Edit an Account with Id: {Id}");

                var account = await _accountRepository.EditAccount(Id, accountdto);

                _logger.LogInformation($"{user.UserName} with email {user.Email} " +
                    $"Edited Account with Id: {Id}, and new balance is {account.Balance}");

                return Ok(account.toAccountDTO());
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError($"Edit Account Failed");
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Edit Account Failed");
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> DeleteAccount([FromRoute] int Id)
        {
            try
            {
                var userId = User.GetUserId();
                var user = await _userManager.FindByIdAsync(userId);

                _logger.LogWarning($"{user.UserName} with email {user.Email} " +
                    $"is trying to Delete an Account with Id: {Id}");

                var account = await _accountRepository.DeleteAccount(Id);

                _logger.LogInformation($"{user.UserName} with email {user.Email} " +
                    $"Deleted the Account with Id: {Id}");

                return Ok(account.toAccountDTO());
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError($"FAILED TO DELETE ACCOUNT: {ex.Message}");
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch(InvalidOperationException ex)
            {
                _logger.LogError($"FAILED TO DELETE ACCOUNT: {ex.Message}");
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"FAILED TO DELETE ACCOUNT: {ex.Message}");
                return BadRequest(new
                {
                    message = ex.Message
                });
            }

        }

        [HttpPut("CloseAccount/{Id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> CloseAccount([FromRoute] int Id)
        {
            try
            {
                var userId = User.GetUserId();
                var user = await _userManager.FindByIdAsync(userId);

                _logger.LogWarning($"{user.UserName} with email {user.Email} " +
                $"is trying to Close an Account with Id: {Id}");

                var account = await _accountRepository.CloseAccount(Id);

                _logger.LogInformation($"{user.UserName} with email {user.Email} " +
                $"Closed the Account with Id: {Id}");

                return Ok(account.toAccountDTO());
            }

            catch(KeyNotFoundException ex)
            {
                _logger.LogError($"FAILED TO CLOSE ACCOUNT: {ex.Message}");
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"FAILED TO CLOSE ACCOUNT: {ex.Message}");
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"FAILED TO CLOSE ACCOUNT: {ex.Message}");
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("FreezeAccount/{Id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> FreezeAccount([FromRoute] int Id)
        {
            try
            {
                var userId = User.GetUserId();
                var user = await _userManager.FindByIdAsync(userId);

                _logger.LogWarning($"{user.UserName} with email {user.Email} " +
                $"is trying to Freeze an Account with Id: {Id}");

                var account = await _accountRepository.FreezeAccount(Id);

                _logger.LogInformation($"{user.UserName} with email {user.Email} " +
                $"Freezed the Account with Id: {Id}");

                return Ok(account.toAccountDTO());
            }
            catch(KeyNotFoundException ex)
            {
                _logger.LogError($"FAILED TO FREEZE ACCOUNT: {ex.Message}");
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"FAILED TO FREEZE ACCOUNT: {ex.Message}");
                return BadRequest(new
                {
                    message = ex.Message
                });
            }

        }

        [HttpPut("ReActivateAccount/{Id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> ReactivateAccount([FromRoute] int Id)
        {
            try
            {
                var userId = User.GetUserId();
                var user = await _userManager.FindByIdAsync(userId);

                _logger.LogWarning($"{user.UserName} with email {user.Email} " +
                $"is trying to Reactivate an Account with Id: {Id}");

                var account = await _accountRepository.ReActivateAccount(Id);

                _logger.LogInformation($"{user.UserName} with email {user.Email} " +
                $"Reactivated the Account with Id: {Id}");

                return Ok(account.toAccountDTO());
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError($"FAILED TO REACTIVATE ACCOUNT: {ex.Message}");
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch(InvalidOperationException ex)
            {
                _logger.LogError($"FAILED TO REACTIVATE ACCOUNT: {ex.Message}");
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"FAILED TO REACTIVATE ACCOUNT: {ex.Message}");
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
