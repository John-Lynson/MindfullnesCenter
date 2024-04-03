using MFC.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFC.Core.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }
    }
}
