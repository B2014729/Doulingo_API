using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.Section;
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
    public class SectionController : ControllerBase
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly ICourseRepository _courseRepository;
        public SectionController(ISectionRepository sectionRepository, ICourseRepository courseRepository)
        {
            _sectionRepository = sectionRepository;
            _courseRepository = courseRepository;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllSection()
        {
            var sections = await _sectionRepository.GetAllInfor();
            if (!sections.Any()) //Sections.Count() == 0
            {
                return Ok(new List<Section>());
            }
            var sectionDTos = sections.Select(u => u.ToSectionDto());
            return Ok(sectionDTos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSection([FromRoute] int id)
        {
            var section = await _sectionRepository.GetSectionDetail(id);
            if (section == null)
            {
                return NotFound();
            }
            return Ok(section.ToSectionDto());
        }

        [HttpPost("create/{courseId}")]
        public async Task<IActionResult> Create([FromRoute] int courseId, [FromBody] SectionRequestCreate obj)
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

            if (await _courseRepository.CourseExist(courseId) == false)
            {
                return NotFound();
            }
            else
            {
                Section section;
                Course? course = await _courseRepository.GetById(c => c.Id == courseId);
                if (course != null)
                {
                    section = obj.ToSectionFromSectionCreateDto(course);
                }
                else
                {
                    section = obj.ToSectionFromSectionCreateDto(new Course());
                }

                await _sectionRepository.Create(section);
                await _sectionRepository.Save();
                // return Ok(Section.TouserDto());
                return CreatedAtAction(nameof(GetSection), new { id = section.Id }, section.ToSectionDto());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditSection([FromRoute] int id, [FromBody] SectionRequestUpdate objUpdate)
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

            if (await _courseRepository.CourseExist(objUpdate.CourseId) == false)
            {
                return NotFound();
            }

            Section? sectionUpdated = await _sectionRepository.Update(id, objUpdate);
            if (sectionUpdated == null)
            {
                return BadRequest();
            }
            await _sectionRepository.Save();

            return CreatedAtAction(nameof(GetSection), new { id = sectionUpdated.Id }, sectionUpdated.ToSectionDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection([FromRoute] int id)
        {
            Section? Section = await _sectionRepository.GetById(l => l.Id == id);
            if (Section == null)
            {
                return NotFound();
            }
            _sectionRepository.Remove(Section);
            await _sectionRepository.Save();

            return CreatedAtAction(nameof(GetSection), new { id = id }, Section.ToSectionDto());
        }
    }
}