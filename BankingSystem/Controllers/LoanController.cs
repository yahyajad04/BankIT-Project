using BankingSystem.DTO_s.LoanDTO_s;
using BankingSystem.Extensions;
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
    public class LoanController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILoanRepository _loanRepository;
        private readonly ILogger<LoanController> _logger;

        public LoanController(UserManager<AppUser> userManager, ILoanRepository loanRepository
            , ILogger<LoanController> logger)
        {
            _userManager = userManager;
            _loanRepository = loanRepository;
            _logger = logger;
        }

        [HttpGet("GetAllYourLoans/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetAllLoans([FromRoute] int Id)
        {
            try
            {
                var userId = User.GetUserId();
                var user = await _userManager.Users.Include(u => u.CustomerProfile)
                    .FirstOrDefaultAsync(c => c.Id == userId);
                var ProfileId = -1;
                if (user.CustomerProfile != null)
                {
                    ProfileId = user.CustomerProfile.Id;
                }
                var Loans = await _loanRepository.GetAll(ProfileId, Id);
                var LoansDTO = Loans.Select(l => l.toLoanDTO());

                if(ProfileId == -1)
                    LoansDTO = Loans.Select(l => l.toLoanFromEmployeeDTO());

                return Ok(LoansDTO);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userId = User.GetUserId();
                var user = await _userManager.Users.Include(u => u.CustomerProfile)
                    .FirstOrDefaultAsync(c => c.Id == userId);

                var ProfileId = -1;
                if (user.CustomerProfile != null)
                {
                    ProfileId = user.CustomerProfile.Id;
                }
                var Loans = await _loanRepository.GetAllLoans(ProfileId);
                var LoansDTO = Loans.Select(l => l.toLoanDTO());

                return Ok(LoansDTO);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
        [HttpGet("GetLoanById/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetLoanById([FromRoute] int Id)
        {
            var loan = await _loanRepository.GetById(Id);

            if (loan == null)
                return NotFound(new
                {
                    message = "This Loan Doesnt Exist"
                });

            return Ok(loan);
        }

        [HttpGet("GetLoanTypes")]
        public async Task<IActionResult> GetLoanTypes()
        {
            var types = await _loanRepository.GetLoanTypes();

            if (types == null)
                return NotFound(new
                {
                    message = "Types are not found"
                });

            return Ok(types);
        }

        [HttpPost("CreateLoan")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> CreateLoan(CreateLoanDTO loandto)
        {
            try
            {
                var userId = User.GetUserId();
                var loanbefore = loandto.fromCreateLoanDTO();
                var user = await _userManager.Users.Include(p => p.CustomerProfile).ThenInclude(a => a.Accounts)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                _logger.LogInformation($"{user.UserName} with email {user.Email} is trying to Create a new Loan" +
                    $" for Account with Id: {loandto.AccountId} and Amount {loandto.Amount}");

                if (user.CustomerProfile != null && !user.CustomerProfile.Accounts.Any(a => a.Id == loandto.AccountId))
                    return Forbid("This Account Doesnt Belong to You");

                var loan = await _loanRepository.CreateLoan(userId, loanbefore);

                _logger.LogInformation($"{user.UserName} with email {user.Email} Created a new Loan" +
                $" for Account with Id: {loandto.AccountId} and Loan Amount {loandto.Amount}");

                return Ok(loan.toLoanDTO());
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError($"FAILED TO CREATE LOAN: {ex.Message}");
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"FAILED TO CREATE LOAN: {ex.Message}");
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("ApproveLoan/{Id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> ApproveLoan(int Id)
        {
            try
            {
                var userId = User.GetUserId();
                var user = await _userManager.FindByIdAsync(userId);

                _logger.LogWarning($"{user.UserName} with email: {user.Email} " +
                    $"is trying to Approve a Loan with Id: {Id}");

                var loan = await _loanRepository.ApproveLoan(Id);

                _logger.LogInformation($"{user.UserName} with email: {user.Email} " +
                    $"Approved the Loan with Id: {Id}");

                return Ok(loan.toLoanDTO());
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError($"FAILED TO APPROVE LOAN: {ex.Message}");
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"FAILED TO APPROVE LOAN: {ex.Message}");
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("DisApproveLoan/{Id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> DisApproveLoan(int Id)
        {
            try
            {
                var userId = User.GetUserId();
                var user = await _userManager.FindByIdAsync(userId);

                _logger.LogWarning($"{user.UserName} with email: {user.Email} " +
                    $"is trying to DisApprove a Loan with Id: {Id}");

                var loan = await _loanRepository.DisApproveLoan(Id);

                _logger.LogInformation($"{user.UserName} with email: {user.Email} " +
                    $"DisApproved the Loan with Id: {Id}");

                return Ok(loan.toLoanDTO());
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError($"FAILED TO DISAPPROVE LOAN: {ex.Message}");
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"FAILED TO DISAPPROVE LOAN: {ex.Message}");
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("MonthlyPay/{Id}")]
        [Authorize]
        public async Task<IActionResult> MonthlyPay(int Id)
        {
            try
            {
                var userId = User.GetUserId();
                var user = await _userManager.FindByIdAsync(userId);

                _logger.LogWarning($"{user.UserName} with email: {user.Email} " +
                    $"is trying to Pay his monthly dept for the Loan with the ID: {Id}");

                var loan = await _loanRepository.MonthlyPayLoan(userId, Id);

                _logger.LogInformation($"{user.UserName} with email: {user.Email} " +
                    $"Payed his monthly dept for the Loan with the ID: {Id}");

                return Ok(loan.toLoanDTO());
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError($"FAILED TO PAY MONTHLY DEPT: {ex.Message}");
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"FAILED TO PAY MONTHLY DEPT: {ex.Message}");
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    } 
}
