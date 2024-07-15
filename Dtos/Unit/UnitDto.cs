using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.Lesson;

namespace Doulingo_Api.Dtos.Unit
{
    public class UnitDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Index { get; set; }
        public int Point { get; set; }
        public DateTime At_Created { set; get; }
        public DateTime At_Updated { set; get; }
        public int SectionId { get; set; }
        public List<LessonDto> Lessons { get; set; } = new List<LessonDto>();
    }
}