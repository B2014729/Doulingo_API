using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doulingo_Api.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageSrc { get; set; } = string.Empty;
        public DateTime At_Created { set; get; } = DateTime.Now;
        public DateTime At_Updated { set; get; } = DateTime.Now;
        public ICollection<UserProgress> UserProgresses { get; set; } = new List<UserProgress>();
        public ICollection<Section> Sections { get; set; } = new List<Section>();
    }
}

// {
//     Id: 1,
//     Title: "VietNamese",
//     ImageSrc: "./images/vietname.png",
// }