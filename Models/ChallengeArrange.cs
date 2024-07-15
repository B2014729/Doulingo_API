using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doulingo_Api.Models
{
    public class ChallengeArrange
    {
        public int Id { get; set; }
        public string Type { get; } = "Arrange";
        public string Question { get; set; } = string.Empty;
        public string Options { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public string? SoundSrc { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; } = new Lesson();
        public DateTime At_Created { set; get; } = DateTime.Now;
        public DateTime At_Updated { set; get; } = DateTime.Now;
    }
}

// {
//     Id: 1,
//     Type: "Arrange",
//     Question: "Hello, I am Bang",
//     Options: "Tôi, Xin chào, tên, là, Băng, Dương",
//     Answer: "Xin chào Tôi là Băng",
//     SoundSrc: null,
//     LessonId: 1 //This is a question for lesson 1
// }

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
