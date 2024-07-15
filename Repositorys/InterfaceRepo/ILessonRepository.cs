using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.Lesson;
using Doulingo_Api.Models;

namespace Doulingo_Api.Repositorys.InterfaceRepo
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Task<Lesson?> Update(int id, LessonRequestUpdate lesson);
        int GetLessonWithUserProgress(Course course, int point);
        Task<bool> LessonExist(int id);
        Task<List<Lesson>> GetAllInfor();
        Task<Lesson?> GetLessonDetail(int id);
    }
}