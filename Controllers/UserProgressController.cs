using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.UserProgress;
using Doulingo_Api.Mappers;
using Doulingo_Api.Models;
using Doulingo_Api.Repositorys.InterfaceRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Doulingo_Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserProgressController : ControllerBase
    {
        private readonly IUserProgressRepository _userProgressRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        public UserProgressController(IUserProgressRepository userProgressRepository, IUserRepository userRepository, ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _userProgressRepository = userProgressRepository;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllUserProgress()
        {
            var userProgesses = await _userProgressRepository.GetAll();
            if (!userProgesses.Any()) //userProgesses.Count() == 0
            {
                return Ok(new List<UserProgress>());
            }
            var userProgressDtos = userProgesses.Select(u => u.ToUserProgressDto());
            return Ok(userProgressDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProgress([FromRoute] int id)
        {
            var userProgess = await _userProgressRepository.GetById(u => u.Id == id);
            if (userProgess == null)
            {
                return NotFound();
            }
            return Ok(userProgess.ToUserProgressDto());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserProgressRequestCreate obj)
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

            if (await _courseRepository.CourseExist(obj.CourseId) == false)
            {
                return NotFound();
            }

            else if (await _userRepository.UserExist(obj.UserId) == false)
            {
                return NotFound();
            }
            else
            {
                UserProgress userProgress;
                Course? course = await _courseRepository.GetById(c => c.Id == obj.CourseId);
                User? user = await _userRepository.GetById(c => c.Id == obj.UserId);

                if (course != null && user != null)
                {
                    userProgress = obj.ToUserProgressFromUserProgressCreateDto(user, course);
                }
                else
                {
                    userProgress = obj.ToUserProgressFromUserProgressCreateDto(new User(), new Course());
                }

                await _userProgressRepository.Create(userProgress);
                await _userProgressRepository.Save();

                return CreatedAtAction(nameof(GetUserProgress), new { id = userProgress.Id }, userProgress.ToUserProgressDto());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUserProgress([FromRoute] int id, [FromBody] UserProgressRequestUpdate objUpdate)
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

            if (await _userRepository.UserExist(objUpdate.UserId) == false)
            {
                return NotFound();
            }

            UserProgress? userProgressUpdated = await _userProgressRepository.Update(id, objUpdate);
            if (userProgressUpdated == null)
            {
                return BadRequest();
            }
            await _userProgressRepository.Save();

            return CreatedAtAction(nameof(GetUserProgress), new { id = userProgressUpdated.Id }, userProgressUpdated.ToUserProgressDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProgress([FromRoute] int id)
        {
            UserProgress? userProgress = await _userProgressRepository.GetById(l => l.Id == id);
            if (userProgress == null)
            {
                return NotFound();
            }
            _userProgressRepository.Remove(userProgress);
            await _userProgressRepository.Save();

            return CreatedAtAction(nameof(GetUserProgress), new { id = id }, userProgress.ToUserProgressDto());
        }

        [HttpGet("{userId}/{courseId}")]
        public async Task<IActionResult> GetUserProgressWhere(int userId, int courseId)
        {
            //string token; => userId

            var userProgress = await _userProgressRepository.GetUserProgressWithCourse(userId, courseId);
            if (userProgress == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetUserProgress), new { id = userId }, userProgress.ToUserProgressDto());
        }
    }
}