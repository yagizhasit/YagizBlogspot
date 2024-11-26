using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class BlogTag 
    {
        public int ID {  get; set; }
        public int BlogID { get; set; }
        public Blog Blog { get; set; }
        public int TagID { get; set; }
        public Tag Tag { get; set; }

    }
}
