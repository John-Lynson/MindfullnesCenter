using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFC.CORE.Models
{
    public class ForumPost
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } // Verbinding met User
        public User User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateTimePosted { get; set; }
    }
}
