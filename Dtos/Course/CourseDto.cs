using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.Section;


namespace Doulingo_Api.Dtos.Course
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageSrc { get; set; } = string.Empty;
        public DateTime At_Created { get; set; }
        public DateTime At_Updated { get; set; }
        public List<SectionDto> Sections { get; set; } = new List<SectionDto>();
    }
}