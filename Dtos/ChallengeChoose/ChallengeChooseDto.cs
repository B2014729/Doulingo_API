using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doulingo_Api.Dtos.ChallengeChoose
{
    public class ChallengeChooseDto
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
        public DateTime At_Created { set; get; } = DateTime.Now;
        public DateTime At_Updated { set; get; } = DateTime.Now;
    }
}