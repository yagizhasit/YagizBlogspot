using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<Blog> Blogs { get; set; } = new List<Blog>();

    }
}
