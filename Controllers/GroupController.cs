using FinancasMVC.Models;
using FinancasMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancasMVC.Controllers
{
    [Route("api/group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly GroupService _groupService;
        private readonly SubGroupService _subGroupService;

        public GroupController(GroupService groupService, SubGroupService subGroupService)
        {
            _groupService = groupService;
            _subGroupService = subGroupService;
        }

        [HttpGet]
        public ActionResult<List<Group>> Get()
        {
            var models = _groupService.GetAll();

            return models.ToList();
        }

        [HttpGet("{id:length(36)}")]
        public ActionResult<Group> Get(Guid id)
        {
            var model = _groupService.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        [HttpPost]
        public ActionResult<Group> Create(Group model)
        {
            _groupService.Insert(model);

            return CreatedAtRoute("GetGroup", new { id = model.Id.ToString() }, model);
        }

        [HttpPut("{id:length(36)}")]
        public IActionResult Update(Guid id, Group modelIn)
        {
            var model = _groupService.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            _groupService.Update(modelIn);

            return NoContent();
        }

        [HttpDelete("{id:length(36)}")]
        public IActionResult Delete(Guid id)
        {
            var model = _groupService.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            var childs = _subGroupService.GetByGroupId(model.Id);

            if (childs.Any())
            {
                return BadRequest("Grupo já está sendo usado em um subgrupo!");
            }

            _groupService.Remove(model.Id);

            return NoContent();
        }
    }
}