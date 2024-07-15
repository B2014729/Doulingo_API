using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.User;
using Doulingo_Api.Models;

namespace Doulingo_Api.Repositorys.InterfaceRepo
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> Update(int id, UserRequestUpdate entity);
        Task<User?> ChangePassword(int id, string newpassword);
        Task<bool> UserExist(int id);
        Task<User?> CheckUserLogin(string Email, string Password);

        string GenerateToken(User user);
    }
}