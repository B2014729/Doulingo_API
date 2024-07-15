using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doulingo_Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; } = string.Empty;
        public DateTime At_Created { set; get; } = DateTime.Now;
        public DateTime At_Updated { set; get; } = DateTime.Now;
        public ICollection<UserProgress> UserProgresses { get; set; } = new List<UserProgress>();
    }
}

// {
//     Id: 1, 
//     Username: "haibang",
//     Email: "example123@gmail.com",
//     Password: "********",    
//     Role: "Administration"
//     AvatarUrl: ""
// }