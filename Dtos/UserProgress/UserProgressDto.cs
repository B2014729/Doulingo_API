using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doulingo_Api.Dtos.UserProgress
{
    public class UserProgressDto
    {
        public int Id { get; set; }
        public int Point { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime At_Created { set; get; } = DateTime.Now;
        public DateTime At_Updated { set; get; } = DateTime.Now;
    }
}