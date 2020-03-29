using FinancasMVC.Models;
using FinancasMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancasMVC.Controllers
{
    [Route("api/subgroup")]
    [ApiController]
    public class SubGroupController : ControllerBase
    {
        private readonly SubGroupService _subGroupService;
        private readonly TransactionService _transactionService;

        public SubGroupController(SubGroupService subGroupService, TransactionService transactionService)
        {
            _subGroupService = subGroupService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public ActionResult<List<SubGroup>> Get()
        {
            var models = _subGroupService.GetAll();

            return models.ToList();
        }

        [HttpGet("{id:length(36)}")]
        public ActionResult<SubGroup> Get(Guid id)
        {
            var model = _subGroupService.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        [HttpPost]
        public ActionResult<SubGroup> Create(SubGroup model)
        {
            _subGroupService.Insert(model);

            return CreatedAtRoute("GetSubGroup", new { id = model.Id.ToString() }, model);
        }

        [HttpPut("{id:length(36)}")]
        public IActionResult Update(Guid id, SubGroup modelIn)
        {
            var model = _subGroupService.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            _subGroupService.Update(modelIn);

            return NoContent();
        }

        [HttpDelete("{id:length(36)}")]
        public IActionResult Delete(Guid id)
        {
            var model = _subGroupService.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            var childs = _transactionService.GetByAccountId(model.Id);

            if (childs.Any())
            {
                return BadRequest("Subgrupo já está sendo usada em um movimento!");
            }

            _subGroupService.Remove(model.Id);

            return NoContent();
        }
    }
}