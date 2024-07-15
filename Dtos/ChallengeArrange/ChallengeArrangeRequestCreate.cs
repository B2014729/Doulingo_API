using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Doulingo_Api.Dtos.ChallengeArrange
{
    public class ChallengeArrangeRequestCreate
    {
        public string Type { get; } = "Arrange";
        [Required]
        public string Question { get; set; } = string.Empty;
        [Required]
        public string Options { get; set; } = string.Empty;
        [Required]
        public string Answer { get; set; } = string.Empty;
        public string? SoundSrc { get; set; }
        [Required]
        public int LessonId { get; set; }
    }
}