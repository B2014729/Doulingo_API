using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Doulingo_Api.Dtos.ChallengeChoose
{
    public class ChallengeChooseRequestCreate
    {
        [Required]
        public string Question { get; set; } = string.Empty;
        [Required]
        public string Options_A { get; set; } = string.Empty;
        [Required]
        public string Options_B { get; set; } = string.Empty;
        [Required]
        public string Options_C { get; set; } = string.Empty;
        [Required]
        public string Options_D { get; set; } = string.Empty;
        [Required]
        public string Answer { get; set; } = string.Empty;
        [Required]
        public int LessonId { get; set; }
    }
}