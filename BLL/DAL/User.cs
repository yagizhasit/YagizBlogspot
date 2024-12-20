using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class User
    {
        public int ID { get; set; }

        [Required, StringLength(20)]
        public string Username { get; set; }

        [Required, StringLength(10)]
        public string Password { get; set; }

        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<Blog> Blogs { get; set; } = new List<Blog>();

    }
}
