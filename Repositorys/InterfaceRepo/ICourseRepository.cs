using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.Course;
using Doulingo_Api.Models;

namespace Doulingo_Api.Repositorys.InterfaceRepo
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<List<Course>> GetAllInfor();
        Task<Course?> GetCourseDetail(int id);
        Task<Course?> Update(int id, CourseRequestUpdate entity);
        Task<bool> CourseExist(int id);
    }
}