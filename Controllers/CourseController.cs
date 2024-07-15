using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.Course;
using Doulingo_Api.Mappers;
using Doulingo_Api.Models;
using Doulingo_Api.Repositorys.InterfaceRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Doulingo_Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllCourse()
        {
            var courses = await _courseRepository.GetAllInfor();

            if (!courses.Any()) //courses.Count() == 0
            {
                return Ok(new List<Course>());
            }
            var courseDtos = courses.Select(u => u.ToCourseDto());
            return Ok(courseDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse([FromRoute] int id)
        {
            var course = await _courseRepository.GetCourseDetail(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course.ToCourseDto());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CourseRequestCreate obj)
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

            Course course = obj.ToCourseFromCourseCreateDto();
            await _courseRepository.Create(course);
            await _courseRepository.Save();

            // return Ok(course.TouserDto());
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course.ToCourseDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCourse([FromRoute] int id, [FromBody] CourseRequestUpdate objUpdate)
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

            Course? courseUpdated = await _courseRepository.Update(id, objUpdate);
            if (courseUpdated == null)
            {
                return BadRequest();
            }
            await _courseRepository.Save();

            return CreatedAtAction(nameof(GetCourse), new { id = courseUpdated.Id }, courseUpdated.ToCourseDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int id)
        {
            Course? course = await _courseRepository.GetById(u => u.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            _courseRepository.Remove(course);
            await _courseRepository.Save();

            return CreatedAtAction(nameof(GetCourse), new { id = id }, course.ToCourseDto());
        }
    }
}