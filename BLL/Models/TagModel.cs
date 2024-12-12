using BLL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class TagModel
    {
        public Tag Record { get; set; }

        public string Name => Record.Name;


    }
}
