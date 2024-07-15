using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.ChallengeArrange;
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
    public class ChallengeArrangeController : ControllerBase
    {
        private readonly IChallengeArrangeRepository _challengeArrangeRepository;
        private readonly ILessonRepository _lessonRepository;
        public ChallengeArrangeController(IChallengeArrangeRepository challengeArrangeRepository, ILessonRepository lessonRepository)
        {
            _challengeArrangeRepository = challengeArrangeRepository;
            _lessonRepository = lessonRepository;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var challenges = await _challengeArrangeRepository.GetAll();
            if (!challenges.Any()) //challenges.Count() == 0
            {
                return Ok(new List<ChallengeArrange>());
            }
            var challengesArrangDto = challenges.Select(u => u.ToChallengeArrangeDto());
            return Ok(challengesArrangDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChallenge([FromRoute] int id)
        {
            var challenge = await _challengeArrangeRepository.GetById(l => l.Id == id);
            if (challenge == null)
            {
                return NotFound();
            }
            return Ok(challenge.ToChallengeArrangeDto());
        }

        [HttpPost("create/{lessonId}")]
        public async Task<IActionResult> Create([FromRoute] int lessonId, [FromBody] ChallengeArrangeRequestCreate obj)
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

            if (await _lessonRepository.LessonExist(lessonId) == false)
            {
                return NotFound();
            }
            else
            {
                ChallengeArrange challenge;
                Lesson? lesson = await _lessonRepository.GetById(c => c.Id == lessonId);
                if (lesson != null)
                {
                    challenge = obj.ToChallengeArrangeFromChallengeArrangeRequestCreate(lesson);
                }
                else
                {
                    challenge = obj.ToChallengeArrangeFromChallengeArrangeRequestCreate(new Lesson());
                }

                await _challengeArrangeRepository.Create(challenge);
                await _challengeArrangeRepository.Save();
                // return Ok(level.TouserDto());
                return CreatedAtAction(nameof(GetChallenge), new { id = challenge.Id }, challenge.ToChallengeArrangeDto());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editlesson([FromRoute] int id, [FromBody] ChallengeArrangeRequestCreate objUpdate)
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

            if (await _lessonRepository.LessonExist(objUpdate.LessonId) == false)
            {
                return NotFound();
            }

            ChallengeArrange? challengeArrangeUpdated = await _challengeArrangeRepository.Update(id, objUpdate);
            if (challengeArrangeUpdated == null)
            {
                return BadRequest();
            }
            await _challengeArrangeRepository.Save();

            return CreatedAtAction(nameof(GetChallenge), new { id = challengeArrangeUpdated.Id }, challengeArrangeUpdated.ToChallengeArrangeDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLevel([FromRoute] int id)
        {
            ChallengeArrange? challenge = await _challengeArrangeRepository.GetById(l => l.Id == id);
            if (challenge == null)
            {
                return NotFound();
            }
            _challengeArrangeRepository.Remove(challenge);
            await _challengeArrangeRepository.Save();

            return CreatedAtAction(nameof(GetChallenge), new { id = id }, challenge.ToChallengeArrangeDto());
        }
    }
}