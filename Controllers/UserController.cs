using System.Security.Claims;
using Doulingo_Api.Dtos.User;
using Doulingo_Api.Mappers;
using Doulingo_Api.Models;
using Doulingo_Api.Repositorys.InterfaceRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Doulingo_Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userLogin)
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

            User? userModel = await _userRepository.CheckUserLogin(userLogin.Email, userLogin.Password);

            if (userModel == null)
            {
                var errorResponse = new ErrorResponse()
                {
                    StatusCode = 404,
                    Message = "Not Found",
                    Errors = new List<string> { "Email or passeword incorrect!" }
                };
                return NotFound(errorResponse);
            }

            var token = _userRepository.GenerateToken(userModel);

            // return Ok(new
            // {
            //     StatusCode = 200,
            //     Token = token
            // });
            return Ok(token);
        }

        [Authorize]
        [HttpGet("person")]
        public async Task<IActionResult> GetInfor()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaim = identity.Claims;

                var userID = userClaim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                var userCurrent = await _userRepository.GetById(u => u.Id == (Int32.Parse(userID ?? "0")));
                if (userCurrent == null)
                {
                    return NotFound();
                }
                return Ok(userCurrent.ToUserDto());
            }

            var errorResponse = new ErrorResponse()
            {
                StatusCode = 404,
                Message = "Not Found",
                Errors = new List<string> { "User not found!" }
            };
            return Unauthorized(errorResponse);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("list")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userRepository.GetAll();
            if (!users.Any()) //users.Count() == 0
            {
                return Ok(new List<User>());
            }
            var userDtos = users.Select(u => u.ToUserDto());
            return Ok(userDtos);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = await _userRepository.GetById(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ToUserDto());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserRequestCreate obj)
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

            User user = obj.ToUserFromUserCreateDto();
            await _userRepository.Create(user);
            await _userRepository.Save();

            // return Ok(user.ToUserDto());
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user.ToUserDto());
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser([FromRoute] int id, [FromBody] UserRequestUpdate userUpdate)
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

            // User user = userUpdate.ToUserFromUserUpdateDto();
            User? userUpdated = await _userRepository.Update(id, userUpdate);
            if (userUpdated == null)
            {
                return BadRequest();
            }
            await _userRepository.Save();

            return CreatedAtAction(nameof(GetUser), new { id = userUpdated.Id }, userUpdated.ToUserDto());
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            User? user = await _userRepository.GetById(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _userRepository.Remove(user);
            await _userRepository.Save();

            return CreatedAtAction(nameof(GetUser), new { id = id }, user.ToUserDto());
        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordChange passwordNew)
        {
            var id = 1;
            var user = await _userRepository.ChangePassword(id, passwordNew.Password);
            if (user == null)
            {
                return NotFound();
            }
            await _userRepository.Save();

            return CreatedAtAction(nameof(GetUser), new { id = id }, user.ToUserDto());
        }
    }
}