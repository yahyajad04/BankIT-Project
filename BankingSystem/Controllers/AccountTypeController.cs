using BankingSystem.DTO_s.AccountTypeDTO_s;
using BankingSystem.Interfaces;
using BankingSystem.Mappers;
using BankingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTypeController : ControllerBase
    {
        private readonly IAccountTypeRepository _accountTypeRepository;
        public AccountTypeController(IAccountTypeRepository accountTypeRepository)
        {
            _accountTypeRepository = accountTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTypes()
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState); 
            var Types = await _accountTypeRepository.GetAll();
            var TypesDTO = Types.Select(t => t.toAccountTypeDTO());

            return Ok(TypesDTO);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAccountType(CreateAccountTypeDTO typedto)
        {
            try
            {
                var type = typedto.fromCreateAccountTypeDTO();
                var typeDone = await _accountTypeRepository.CreateType(type);

                return Ok(typeDone.toAccountTypeDTO());
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
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

        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> EditType(int Id, EditAccountTypeDTO typedto)
        {
            try
            {
                var type = await _accountTypeRepository.EditType(Id, typedto);
                return Ok(type.toAccountTypeDTO());
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
        public async Task<IActionResult> DeleteType(int Id)
        {
            try
            {
                var type = await _accountTypeRepository.DeleteType(Id);

                if (type == null)
                    return BadRequest("This Type Doesnt Exist or there is Accounts Registered with this Type");

                return Ok(type.toAccountTypeDTO());
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
