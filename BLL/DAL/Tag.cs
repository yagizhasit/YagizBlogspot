using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Tag
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<BlogTag> BlogTags { get; set; } = new List<BlogTag>();

    }
}
