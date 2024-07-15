using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Doulingo_Api.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Index { get; set; }
        public DateTime At_Created { set; get; } = DateTime.Now;
        public DateTime At_Updated { set; get; } = DateTime.Now;
        public int CourseId { get; set; }
        public Course Course { get; set; } = new Course();
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}

// {
//     Id: 1,
//     Title: "Basic level",
//     Index: 1,
//     CourseId: 1 //VietNamese
// }

// {
//     Id: 2,
//     Title: "Medium level",
//     Index: 2,
//     CourseId: 1 //VietNamese
// }