using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Data;
using Doulingo_Api.Dtos.Lesson;
using Doulingo_Api.Models;
using Doulingo_Api.Repositorys.InterfaceRepo;
using Microsoft.EntityFrameworkCore;

namespace Doulingo_Api.Repositorys
{
    public class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        private readonly ApplicationContext _context;
        private readonly ICourseRepository _courseRepository;
        public LessonRepository(ApplicationContext context, ICourseRepository courseRepository) : base(context)
        {
            _context = context;
            _courseRepository = courseRepository;
        }

        public async Task<List<Lesson>> GetAllInfor()
        {
            return await _context.Lessons
                        .Include(l => l.ChallengeChooses)
                        .Include(l => l.ChallengeArranges)
                        .ToListAsync();
        }

        public async Task<Lesson?> GetLessonDetail(int id)
        {
            return await _context.Lessons
                        .Include(l => l.ChallengeChooses)
                        .Include(l => l.ChallengeArranges)
                        .Where(l => l.Id == id)
                        .FirstOrDefaultAsync();
        }

        public int GetLessonWithUserProgress(Course courseInfor, int Point)
        {
            List<Lesson> lessonsInCourse = [];
            List<Section> sectionsInCourse = courseInfor.Sections.ToList();

            var indexSection = 1;
            foreach (var section in sectionsInCourse)
            {
                if (section.Index <= indexSection)
                {
                    var indexUnit = 1;
                    foreach (var unit in section.Units)
                    {
                        if (unit.Index <= indexUnit)
                        {
                            var indexLesson = 1;
                            foreach (var lesson in unit.Lessons)
                            {
                                if (lesson.Index <= indexLesson)
                                {
                                    lessonsInCourse.Add(lesson);
                                }
                                indexLesson += 1;
                            }
                        }
                        indexUnit += 1;
                    }
                }
                indexSection += 1;
            }

            for (int i = 0; i < lessonsInCourse.ToArray().Length; i++)
            {
                Point -= lessonsInCourse[i].Point;
                if (Point < 0)
                {
                    return lessonsInCourse[i].Id;
                }
                if (Point == 0)
                {
                    return lessonsInCourse[i + 1].Id;
                }
            }

            return 0;
        }

        public async Task<bool> LessonExist(int id)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == id);
            if (lesson == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Lesson?> Update(int id, LessonRequestUpdate entity)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == id);
            if (lesson == null)
            {
                return null;
            }

            lesson.Title = entity.Title;
            lesson.Index = entity.Index;
            lesson.Point = entity.Point;
            lesson.UnitId = entity.UnitId;
            lesson.At_Updated = DateTime.Now;

            await _context.SaveChangesAsync();
            return lesson;
            //throw new NotImplementedException();
        }
    }
}