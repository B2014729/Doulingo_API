using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doulingo_Api.Models
{
    public class UserProgress
    {
        public int Id { get; set; }
        public int Point { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public User User { get; set; } = new User();
        public Course Course { get; set; } = new Course();
        public DateTime At_Created { set; get; } = DateTime.Now;
        public DateTime At_Updated { set; get; } = DateTime.Now;
    }
}

// {
//     Id: 1, 
//     Point: 220,
//     UserId: 1,
//     CourseId: 1 //VietNamese
// }