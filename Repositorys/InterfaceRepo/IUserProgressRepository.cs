using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.UserProgress;
using Doulingo_Api.Models;

namespace Doulingo_Api.Repositorys.InterfaceRepo
{
    public interface IUserProgressRepository : IRepository<UserProgress>
    {
        Task<UserProgress?> Update(int id, UserProgressRequestUpdate enity);
        Task<UserProgress?> GetUserProgressWithCourse(int userId, int courseId);
        Task<int> GetPointUserProgress(int userId, int courseId);
    }
}