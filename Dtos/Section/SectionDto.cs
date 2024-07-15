using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doulingo_Api.Dtos.Unit;

namespace Doulingo_Api.Dtos.Section
{
    public class SectionDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Index { get; set; }
        public int CourseId { get; set; }
        public DateTime At_Created { set; get; }
        public DateTime At_Updated { set; get; }
        public List<UnitDto> Units { get; set; } = new List<UnitDto>();
    }
}