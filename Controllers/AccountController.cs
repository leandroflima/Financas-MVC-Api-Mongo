using FinancasMVC.Models;
using FinancasMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancasMVC.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly TransactionService _transactionService;

        public AccountController(AccountService accountService, TransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public ActionResult<List<Account>> Get()
        {
            var models = _accountService.GetAll();

            return models.ToList();
        }

        [HttpGet("{id:length(36)}")]
        public ActionResult<Account> Get(Guid id)
        {
            var model = _accountService.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        [HttpPost]
        public ActionResult<Account> Create(Account model)
        {
            _accountService.Insert(model);

            return CreatedAtRoute("GetAccount", new { id = model.Id.ToString() }, model);
        }

        [HttpPut("{id:length(36)}")]
        public IActionResult Update(Guid id, Account modelIn)
        {
            var model = _accountService.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            _accountService.Update(modelIn);

            return NoContent();
        }

        [HttpDelete("{id:length(36)}")]
        public IActionResult Delete(Guid id)
        {
            var model = _accountService.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            var childs = _transactionService.GetByAccountId(model.Id);

            if (childs.Any())
            {
                return BadRequest("Conta já está sendo usada em um movimento!");
            }

            _accountService.Remove(model.Id);

            return NoContent();
        }
    }
}