using FinancasMVC.Models;
using FinancasMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancasMVC.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public ActionResult<List<Transaction>> Get()
        {
            var models = _transactionService.GetAll();

            return models.ToList();
        }

        [HttpGet("{id:length(36)}")]
        public ActionResult<Transaction> Get(Guid id)
        {
            var model = _transactionService.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        [HttpPost]
        public ActionResult<Transaction> Create(Transaction model)
        {
            _transactionService.Insert(model);

            return CreatedAtRoute("GetTransaction", new { id = model.Id.ToString() }, model);
        }

        [HttpPut("{id:length(36)}")]
        public IActionResult Update(Guid id, Transaction modelIn)
        {
            var model = _transactionService.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            _transactionService.Update(modelIn);

            return NoContent();
        }

        [HttpDelete("{id:length(36)}")]
        public IActionResult Delete(Guid id)
        {
            var model = _transactionService.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            _transactionService.Remove(model.Id);

            return NoContent();
        }
    }
}