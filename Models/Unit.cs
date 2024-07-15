using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doulingo_Api.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Index { get; set; }
        public int Point { get; set; }
        public DateTime At_Created { set; get; } = DateTime.Now;
        public DateTime At_Updated { set; get; } = DateTime.Now;
        public int SectionId { get; set; }
        public Section Section { get; set; } = new Section();
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}

// {
//     Id: 1,
//     Title: "This is the basic lesson",
//     Description: "Basic lesson for beginner",
//     Index: 1,   //The first unit in a level
//     Point: 100,
//     LevelId: 1,  //Level have 2 Unit
// }
// {
//     Id: 2,
//     Title: "This is the lesson medium",
//     Description: "medium lesson for beginner",
//     Index: 2,   //The second unit in a level
//     Point: 100,
//     LevelId: 1, //Level have 22 Unit
// }
