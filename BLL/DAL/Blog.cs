using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Blog
    {
        public int ID {  get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal Rating { get; set; }
        public DateTime PublishDate { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public List<BlogTag> BlogTags { get; set; } = new List<BlogTag>();

    }
}
