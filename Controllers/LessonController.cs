using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.Lesson;
using Doulingo_Api.Dtos.UserProgress;
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
    public class LessonController : ControllerBase
    {
        private readonly IUnitRepository _unitRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserProgressRepository _userProgressRepository;
        public LessonController(IUnitRepository unitRepository, ILessonRepository lessonRepository,
                                IUserRepository userRepository, ICourseRepository courseRepository,
                                IUserProgressRepository userProgressRepository)
        {
            _unitRepository = unitRepository;
            _lessonRepository = lessonRepository;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _userProgressRepository = userProgressRepository;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var lessons = await _lessonRepository.GetAllInfor();
            if (!lessons.Any()) //lessons.Count() == 0
            {
                return Ok(new List<Lesson>());
            }
            var lessonDtos = lessons.Select(u => u.ToLessonDto());
            return Ok(lessonDtos);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetLesson([FromRoute] int id)
        {
            var lesson = await _lessonRepository.GetLessonDetail(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return Ok(lesson.ToLessonDto());
        }

        [HttpPost("create/{unitId}")]
        public async Task<IActionResult> Create([FromRoute] int unitId, [FromBody] LessonRequestCreate obj)
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

            if (await _unitRepository.UnitExist(unitId) == false)
            {
                return NotFound();
            }
            else
            {
                Lesson lesson;
                Unit? unit = await _unitRepository.GetById(c => c.Id == unitId);
                if (unit != null)
                {
                    lesson = obj.ToLessonFromLessonCreateDto(unit);
                }
                else
                {
                    lesson = obj.ToLessonFromLessonCreateDto(new Unit());
                }

                await _lessonRepository.Create(lesson);
                await _lessonRepository.Save();
                // return Ok(level.TouserDto());
                return CreatedAtAction(nameof(GetLesson), new { id = lesson.Id }, lesson.ToLessonDto());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUnit([FromRoute] int id, [FromBody] LessonRequestUpdate objUpdate)
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

            if (await _unitRepository.UnitExist(objUpdate.UnitId) == false)
            {
                return NotFound();
            }

            Lesson? lessonUpdated = await _lessonRepository.Update(id, objUpdate);
            if (lessonUpdated == null)
            {
                return BadRequest();
            }
            await _lessonRepository.Save();

            return CreatedAtAction(nameof(GetLesson), new { id = lessonUpdated.Id }, lessonUpdated.ToLessonDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLevel([FromRoute] int id)
        {
            Lesson? lesson = await _lessonRepository.GetById(l => l.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }
            _lessonRepository.Remove(lesson);
            await _lessonRepository.Save();

            return CreatedAtAction(nameof(GetLesson), new { id = id }, lesson.ToLessonDto());
        }

        [HttpPost("get-lesson")]
        public async Task<IActionResult> GetLessonWithUserProgress([FromBody] UserProgressRequest userProgressRequest)
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

            //token => user;
            User? user = await _userRepository.GetById(u => u.Id == 1);
            Course? course = await _courseRepository.GetCourseDetail(userProgressRequest.CourseId);

            if (user != null && course != null)
            {
                var userProgress = _userProgressRepository.GetPointUserProgress(user.Id, course.Id);

                var lesson = await _lessonRepository.GetById(l => l.Id == _lessonRepository.GetLessonWithUserProgress(course, 180));
                if (lesson != null)
                {
                    return CreatedAtAction(nameof(GetLesson), new { id = lesson.Id }, lesson.ToLessonDto());
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }
    }
}

