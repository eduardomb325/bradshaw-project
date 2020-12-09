using BradshawProject.Objects;
using BradshawProject.Services;
using BradshawProject.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BradshawProject.Controllers
{
    [ApiController]
    [Route("/account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Route("/register")]
        [HttpPost]
        public IActionResult RegisterDataToAccountController([FromBody] Account account)
        {
            try
            {
                if (account.IsValidLimit())
                {
                    var response = _accountService.RegisterDataToAccount(account);

                    return Ok(response);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("/get")]
        [HttpPost]
        public IActionResult GetAccountController()
        {
            try
            {
                var response = _accountService.GetAccountData();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
