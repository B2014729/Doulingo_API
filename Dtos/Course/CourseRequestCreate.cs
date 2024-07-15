using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Doulingo_Api.Dtos.Course
{
    public class CourseRequestCreate
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string ImageSrc { get; set; } = string.Empty;
    }
}