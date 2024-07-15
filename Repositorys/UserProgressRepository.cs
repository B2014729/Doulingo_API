using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Data;
using Doulingo_Api.Dtos.UserProgress;
using Doulingo_Api.Models;
using Doulingo_Api.Repositorys.InterfaceRepo;
using Microsoft.EntityFrameworkCore;

namespace Doulingo_Api.Repositorys
{
    public class UserProgressRepository : Repository<UserProgress>, IUserProgressRepository
    {
        private readonly ApplicationContext _context;
        public UserProgressRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserProgress?> GetUserProgressWithCourse(int userId, int courseId)
        {
            UserProgress? userProgress = await _context.UserProgresses
            .Where(u => u.CourseId == courseId && u.UserId == userId)
            .FirstOrDefaultAsync();

            return userProgress;
        }

        public async Task<int> GetPointUserProgress(int userId, int courseId)
        {
            UserProgress? userProgress = await _context.UserProgresses
            .Where(u => u.CourseId == courseId && u.UserId == userId)
            .FirstOrDefaultAsync();

            return (userProgress != null) ? userProgress.Point : 0;
        }

        public async Task<UserProgress?> Update(int id, UserProgressRequestUpdate entity)
        {
            var userProgress = await _context.UserProgresses.FirstOrDefaultAsync(l => l.Id == id);
            if (userProgress == null)
            {
                return null;
            }

            userProgress.Point = entity.Point;
            userProgress.UserId = entity.UserId;
            userProgress.CourseId = entity.CourseId;
            userProgress.At_Updated = DateTime.Now;

            await _context.SaveChangesAsync();
            return userProgress;
        }
    }
}