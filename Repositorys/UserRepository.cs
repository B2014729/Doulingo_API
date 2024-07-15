using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Doulingo_Api.Data;
using Doulingo_Api.Dtos.User;
using Doulingo_Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Doulingo_Api.Repositorys.InterfaceRepo
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _config;
        public UserRepository(ApplicationContext context, IConfiguration config) : base(context)
        {
            _context = context;
            _config = config;
        }

        public async Task<User?> Update(int id, UserRequestUpdate entity)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return null;
            }

            user.Username = entity.Username;
            user.Email = entity.Email;
            user.At_Updated = DateTime.Now;

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> ChangePassword(int id, string newpassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return null;
            }

            user.Password = newpassword;
            user.At_Updated = DateTime.Now;

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExist(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<User?> CheckUserLogin(string Email, string Password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Email && u.Password == Password);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123123Haibangduong123123Haibangduong"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                // issuer: "localhost",
                // audience: "audience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}