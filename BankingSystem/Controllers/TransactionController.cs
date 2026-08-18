using BankingSystem.DTO_s.Account;
using BankingSystem.DTO_s.TransactionDTO_s;
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
using System.Data;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<TransactionController> _logger;
        public TransactionController(UserManager<AppUser> userManager,
            ITransactionRepository transactionRepository,
            ILogger<TransactionController> logger)
        {
            _userManager = userManager;
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        [HttpGet("GetAll/{AccountId}")]
        [Authorize]
        public async Task<IActionResult> GetAllTransactions([FromQuery] QueryObject query, [FromRoute] int AccountId)
        {
            var userId = User.GetUserId();
            var user = await _userManager.Users.Include(c => c.CustomerProfile)
                .FirstOrDefaultAsync(u => u.Id.Equals(userId));
            var profileId = -1;
            if(user.CustomerProfile != null)
                profileId = user.CustomerProfile.Id;
            var transactions = await _transactionRepository.GetAllTransactions(query, AccountId, profileId);
            var transactionsdto = transactions.Select(t => t.toTransactionDTO()).ToList();
            return Ok(transactionsdto);
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var userId = User.GetUserId();
            var user = await _userManager.Users.Include(c => c.CustomerProfile)
                .FirstOrDefaultAsync(u => u.Id.Equals(userId));
            var profileId = -1;
            if (user.CustomerProfile != null)
                profileId = user.CustomerProfile.Id;

            var transactions = await _transactionRepository.GetAll(query,profileId);
            var transactionsdto = transactions.Select(t => t.toTransactionDTO()).ToList();

            return Ok(transactionsdto);
        }

        [HttpGet("GetById/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetTransactionById([FromRoute] int Id)
        {
            var transaction = await _transactionRepository.GetTransactionById(Id);
            return Ok(transaction.toTransactionDTO());
        }

        [HttpPost("Transfer")]
        [Authorize]
        public async Task<IActionResult> CreateTransAction(CreateTransactionDTO transactionDTO)
        {
            try
            {
                var transaction = transactionDTO.fromCreateTransactionDTO();
                var userId = User.GetUserId();
                var user = await _userManager.Users.Include(c => c.CustomerProfile)
                    .ThenInclude(a => a.Accounts).FirstOrDefaultAsync(u => u.Id == userId);
                _logger.LogInformation($"{user.UserName} with the Email: {user.Email} is trying to initiate a transfer transaction");
                var roles = await _userManager.GetRolesAsync(user);

                if (roles[0].Equals("Customer"))
                {
                    var accounts = user.CustomerProfile.Accounts.FirstOrDefault(a => a.Id == transaction.FromAccountId);
                    if (accounts == null)
                        return BadRequest("This Account Doesnt Belong to You");
                }


                transaction.PerformedById = userId;
                transaction.PerformedBy = user;
                var created = await _transactionRepository.CreateTransferTransaction(transaction);
                _logger.LogInformation($"{user.UserName} with the Email: {user.Email} , has created the transfer transaction successfully");
                return Ok(created.toTransactionDTO());
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex) { 
                return BadRequest(new
                {
                    message = ex.Message
                });
            }

        }
        [HttpPost("Deposit")]
        [Authorize]
        public async Task<IActionResult> CreateDepositTransAction(CreateDepositTransactionDTO transactionDTO)
        {
            try
            {
                var transaction = transactionDTO.fromCreateDepositTransactionDTO();
                var userId = User.GetUserId();
                var user = await _userManager.FindByIdAsync(userId);
                _logger.LogInformation($"{user.UserName} with the Email: {user.Email} is trying to initiate a deposit transaction");
                transaction.PerformedById = userId;
                transaction.PerformedBy = user;

                var created = await _transactionRepository.CreateDepositTransaction(transaction);
                _logger.LogInformation($"{user.UserName} with the Email: {user.Email} , has created the deposit transaction successfully");
                return Ok(created.toTransactionDTO());
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
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

        [HttpPost("WithDraw")]
        [Authorize]
        public async Task<IActionResult> CreateWithdrawTransAction(CreateWithdrawTransactionDTO transactionDTO)
        {
            try
            {
                var transaction = transactionDTO.fromCreateWithdrawTransactionDTO();
                var userId = User.GetUserId();
                var user = await _userManager.Users.Include(c => c.CustomerProfile)
                    .ThenInclude(a => a.Accounts).FirstOrDefaultAsync(u => u.Id == userId);
                _logger.LogInformation($"{user.UserName} with the Email: {user.Email} is trying to initiate a withdraw transaction");
                var roles = await _userManager.GetRolesAsync(user);

                if (roles[0].Equals("Customer"))
                {
                    var accounts = user.CustomerProfile.Accounts.FirstOrDefault(a => a.Id == transaction.FromAccountId);
                    if (accounts == null)
                        return BadRequest("This Account Doesnt Belong to You");
                }

                transaction.PerformedById = userId;
                transaction.PerformedBy = user;
                var created = await _transactionRepository.CreateWithdrawTransaction(transaction);
                _logger.LogInformation($"{user.UserName} with the Email: {user.Email} , has created the withdraw transaction successfully");
                return Ok(created.toTransactionDTO());
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
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


        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] int Id)
        {
            try
            {
                var transaction = await _transactionRepository.DeleteTransaction(Id);
                return Ok(transaction.toTransactionDTO());
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
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

        [HttpGet("AbortTransaction/{Id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> AbortTransaction([FromRoute] int Id)
        {
            try
            {
                var userId = User.GetUserId();
                var user = await _userManager.FindByIdAsync(userId);
                _logger.LogWarning($"{user.UserName} with the Email: {user.Email} trying to Abort transaction with Id: {Id}");
                var transaction = await _transactionRepository.AbortTransaction(Id);
                _logger.LogInformation($"{user.UserName} with the Email: {user.Email} Aborted transaction with Id: {Id}");
                return Ok(transaction.toTransactionDTO());
            }
            catch(KeyNotFoundException ex)
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
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }

        }

        [HttpGet("CommitTransaction/{Id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> CommitTransaction([FromRoute] int Id)
        {
            try
            {
                var userId = User.GetUserId();
                var user = await _userManager.FindByIdAsync(userId);
                _logger.LogWarning($"{user.UserName} with the Email: {user.Email} trying to Commit transaction with Id: {Id}");
                var transaction = await _transactionRepository.CommitTransaction(Id);
                _logger.LogInformation($"{user.UserName} with the Email: {user.Email} Commited transaction with Id: {Id}");
                return Ok(transaction.toTransactionDTO());
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
