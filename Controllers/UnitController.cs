using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.Unit;
using Doulingo_Api.Mappers;
using Doulingo_Api.Models;
using Doulingo_Api.Repositorys.InterfaceRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Doulingo_Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UnitController : ControllerBase
    {
        private readonly IUnitRepository _unitRepository;
        private readonly ISectionRepository _sectionRepository;
        public UnitController(IUnitRepository unitRepository, ISectionRepository sectionRepository)
        {
            _unitRepository = unitRepository;
            _sectionRepository = sectionRepository;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var units = await _unitRepository.GetAllInfor();
            if (!units.Any()) //units.Count() == 0
            {
                return Ok(new List<Unit>());
            }
            var unitDtos = units.Select(u => u.ToUnitDto());
            return Ok(unitDtos);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnit([FromRoute] int id)
        {
            var unit = await _unitRepository.GetUnitDetail(id);
            if (unit == null)
            {
                return NotFound();
            }
            return Ok(unit.ToUnitDto());
        }

        [HttpPost("create/{sectionId}")]
        public async Task<IActionResult> Create([FromRoute] int sectionId, [FromBody] UnitRequestCreate obj)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = new ErrorResponse()
                {
                    StatusCode = 400,
                    Message = "Validation failed",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                };
                return BadRequest(errorResponse);
            }

            if (await _sectionRepository.SectionExist(sectionId) == false)
            {
                return NotFound();
            }
            else
            {
                Unit unit;
                Section? section = await _sectionRepository.GetById(c => c.Id == sectionId);
                if (section != null)
                {
                    unit = obj.ToUnitFromUnitCreateDto(section);
                }
                else
                {
                    unit = obj.ToUnitFromUnitCreateDto(new Section());
                }

                await _unitRepository.Create(unit);
                await _unitRepository.Save();
                // return Ok(Section.TouserDto());
                return CreatedAtAction(nameof(GetUnit), new { id = unit.Id }, unit.ToUnitDto());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUnit([FromRoute] int id, [FromBody] UnitRequestUpdate objUpdate)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = new ErrorResponse()
                {
                    StatusCode = 400,
                    Message = "Validation failed",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                };
                return BadRequest(errorResponse);
            }

            if (await _sectionRepository.SectionExist(objUpdate.SectionId) == false)
            {
                return NotFound();
            }

            Unit? unitUpdated = await _unitRepository.Update(id, objUpdate);
            if (unitUpdated == null)
            {
                return BadRequest();
            }
            await _unitRepository.Save();

            return CreatedAtAction(nameof(GetUnit), new { id = unitUpdated.Id }, unitUpdated.ToUnitDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection([FromRoute] int id)
        {
            Unit? unit = await _unitRepository.GetById(l => l.Id == id);
            if (unit == null)
            {
                return NotFound();
            }
            _unitRepository.Remove(unit);
            await _unitRepository.Save();

            return CreatedAtAction(nameof(GetUnit), new { id = id }, unit.ToUnitDto());
        }
    }
}