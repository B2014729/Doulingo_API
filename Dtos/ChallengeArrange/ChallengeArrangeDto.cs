using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doulingo_Api.Dtos.ChallengeArrange
{
    public class ChallengeArrangeDto
    {
        public int Id { get; set; }
        public string Type { get; } = "Arrange";
        public string Question { get; set; } = string.Empty;
        public string Options { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public string? SoundSrc { get; set; }
        public int LessonId { get; set; }
        public DateTime At_Created { set; get; } = DateTime.Now;
        public DateTime At_Updated { set; get; } = DateTime.Now;
    }
}