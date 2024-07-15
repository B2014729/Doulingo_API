using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.ChallengeArrange;
using Doulingo_Api.Dtos.ChallengeChoose;
using Doulingo_Api.Models;

namespace Doulingo_Api.Dtos.Lesson
{
    public class LessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Index { get; set; }
        public int Point { get; set; }
        public int UnitId { get; set; }
        public DateTime At_Created { set; get; }
        public DateTime At_Updated { set; get; }
        public List<ChallengeArrangeDto> ChallengeArranges { get; set; } = new List<ChallengeArrangeDto>();
        public List<ChallengeChooseDto> ChallengChooses { get; set; } = new List<ChallengeChooseDto>();
    }
}