using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Data;
using Doulingo_Api.Dtos.Course;
using Doulingo_Api.Models;
using Doulingo_Api.Repositorys.InterfaceRepo;
using Microsoft.EntityFrameworkCore;

namespace Doulingo_Api.Repositorys
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly ApplicationContext _context;
        public CourseRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetAllInfor()
        {
            return await _context.Courses
            .Include(c => c.Sections)
            .ThenInclude(u => u.Units)
            .ThenInclude(l => l.Lessons)
            .ToListAsync();
        }

        public async Task<Course?> GetCourseDetail(int id)
        {
            return await _context.Courses
            .Include(c => c.Sections)
            .ThenInclude(u => u.Units)
            .ThenInclude(l => l.Lessons)
            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> CourseExist(int id)
        {
            Course? course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Course?> Update(int id, CourseRequestUpdate entity)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(u => u.Id == id);
            if (course == null)
            {
                return null;
            }

            course.Title = entity.Title;
            course.ImageSrc = entity.ImageSrc;
            course.At_Updated = DateTime.Now;

            await _context.SaveChangesAsync();
            return course;
        }
    }
}
