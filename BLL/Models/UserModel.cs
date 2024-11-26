using BLL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class UserModel
    {
        public User Record { get; set; }

        public string Username => Record.Username;

        public string Password => Record.Password;

        public string IsActive => Record.IsActive ? "Yes" : "No";

        public string UserRole => Record.Role.Name;

    }
}
