using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.ChallengeChoose;
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
    public class ChallengeChooseController : ControllerBase
    {
        private readonly IChallengeChooseRepository _challengeChooseRepository;
        private readonly ILessonRepository _lessonRepository;
        public ChallengeChooseController(IChallengeChooseRepository challengeChooseRepository, ILessonRepository lessonRepository)
        {
            _challengeChooseRepository = challengeChooseRepository;
            _lessonRepository = lessonRepository;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var challenges = await _challengeChooseRepository.GetAll();
            if (!challenges.Any()) //challenges.Count() == 0
            {
                return Ok(new List<ChallengeChoose>());
            }
            var challengesArrangDto = challenges.Select(u => u.ToChallengeChooseDto());
            return Ok(challengesArrangDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChallenge([FromRoute] int id)
        {
            var challenge = await _challengeChooseRepository.GetById(l => l.Id == id);
            if (challenge == null)
            {
                return NotFound();
            }
            return Ok(challenge.ToChallengeChooseDto());
        }

        [HttpPost("create/{lessonId}")]
        public async Task<IActionResult> Create([FromRoute] int lessonId, [FromBody] ChallengeChooseRequestCreate obj)
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
                ChallengeChoose challenge;
                Lesson? lesson = await _lessonRepository.GetById(c => c.Id == lessonId);
                if (lesson != null)
                {
                    challenge = obj.ToChallengeChooseFromChallengeChooseRequestCreate(lesson);
                }
                else
                {
                    challenge = obj.ToChallengeChooseFromChallengeChooseRequestCreate(new Lesson());
                }

                await _challengeChooseRepository.Create(challenge);
                await _challengeChooseRepository.Save();
                // return Ok(level.TouserDto());
                return CreatedAtAction(nameof(GetChallenge), new { id = challenge.Id }, challenge.ToChallengeChooseDto());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editlesson([FromRoute] int id, [FromBody] ChallengeChooseRequestCreate objUpdate)
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

            ChallengeChoose? challengeChooseUpdated = await _challengeChooseRepository.Update(id, objUpdate);
            if (challengeChooseUpdated == null)
            {
                return BadRequest();
            }
            await _challengeChooseRepository.Save();

            return CreatedAtAction(nameof(GetChallenge), new { id = challengeChooseUpdated.Id }, challengeChooseUpdated.ToChallengeChooseDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLevel([FromRoute] int id)
        {
            ChallengeChoose? challenge = await _challengeChooseRepository.GetById(l => l.Id == id);
            if (challenge == null)
            {
                return NotFound();
            }
            _challengeChooseRepository.Remove(challenge);
            await _challengeChooseRepository.Save();

            return CreatedAtAction(nameof(GetChallenge), new { id = id }, challenge.ToChallengeChooseDto());
        }
    }
}