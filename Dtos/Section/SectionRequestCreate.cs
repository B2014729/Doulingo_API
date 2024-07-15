using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Doulingo_Api.Dtos.Section
{
    public class SectionRequestCreate
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public int Index { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}