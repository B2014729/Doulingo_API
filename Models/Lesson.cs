using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doulingo_Api.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Index { get; set; }
        public int Point { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; } = new Unit();
        public DateTime At_Created { set; get; } = DateTime.Now;
        public DateTime At_Updated { set; get; } = DateTime.Now;
        public ICollection<ChallengeArrange> ChallengeArranges { get; set; } = new List<ChallengeArrange>();
        public ICollection<ChallengeChoose> ChallengeChooses { get; set; } = new List<ChallengeChoose>();
    }
}

// {
//     Id: 1,
//     Title: "Lesson 1 in Unit 1",
//     Index: 1, //Unit 1 have 4 lesson
//     Point: 20,
//     UnitId: 1,
// }

// {
//     Id: 2,
//     Title: "Lesson 2 in Unit 1",
//     Index: 2, //Unit 1 have 4 lesson
//     Point: 20,
//     UnitId: 1,
// }
// {
//     Id: 3,
//     Title: "Lesson 3 in Unit 1",
//     Index: 3, //Unit 1 have 4 lesson
//     Point: 20,
//     UnitId: 1,
// }
// {
//     Id: 4,
//     Title: "Lesson 4 in Unit 1",
//     Index: 4, //Unit 1 have 4 lesson
//     Point: 20,
//     UnitId: 1,
// }
