using BradshawProject.Domain.Objects;
using BradshawProject.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BradshawProject.Controllers
{
    [ApiController]
    [Route("/transaction")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [Route("/get/lastTransactions")]
        public IActionResult GetLastTransactionsController()
        {
            try
            {
                List<LastTransaction> lastTransactions = _transactionService.GetLastTransactions();

                return Ok(lastTransactions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        [Route("/processTransaction")]
        [HttpPost]
        public IActionResult ProcessTransactionController([FromBody] Transaction transaction)
        {
            try
            {
                LastTransaction lastTransaction = _transactionService.ProcessTransactionService(transaction);

                return Ok(lastTransaction);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
