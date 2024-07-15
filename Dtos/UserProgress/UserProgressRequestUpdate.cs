using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Doulingo_Api.Dtos.UserProgress
{
    public class UserProgressRequestUpdate
    {
        [Required]
        public int Point { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}