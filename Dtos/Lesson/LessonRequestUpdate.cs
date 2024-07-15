using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Doulingo_Api.Dtos.Lesson
{
    public class LessonRequestUpdate
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public int Index { get; set; }
        [Required]
        public int Point { get; set; }
        [Required]
        public int UnitId { get; set; }

    }
}