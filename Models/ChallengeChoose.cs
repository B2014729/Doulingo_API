using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doulingo_Api.Models
{
    public class ChallengeChoose
    {
        public int Id { get; set; }
        public string Type { get; } = "Choose";
        public string Question { get; set; } = string.Empty;
        public string Options_A { get; set; } = string.Empty;
        public string Options_B { get; set; } = string.Empty;
        public string Options_C { get; set; } = string.Empty;
        public string Options_D { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; } = new Lesson();
        public DateTime At_Created { set; get; } = DateTime.Now;
        public DateTime At_Updated { set; get; } = DateTime.Now;
    }
}

// {
// Id: 1,
//     Type: "Choose",
//     Question: "Which one of these is \"the robot\"?",
//     Options_A: "./image/robot.png",
//     Options_B: "./image/man.png",
//     Options_C: "./image/girl.png",
//     Options_D: "./image/boy.png",
//     Answer: "A",
//     LessonId: 1 //This is a question for lesson 1
// }
