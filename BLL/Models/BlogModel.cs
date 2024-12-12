using BLL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class BlogModel
    {
        public Blog Record { get; set; }
        public string Title => Record.Title;
        public string Content => Record.Content;
        public string PublishDate => Record.PublishDate.ToString();
        public decimal Rating => Record.Rating;
        public string User => Record.User?.Username;
        public string Tags => string.Join("<br>", Record.BlogTags?.Select(bt => bt.Tag?.Name));

        [DisplayName("Tags")]
        public List<int> TagIDs
        {
            get => Record.BlogTags?.Select(bt => bt.TagID).ToList();
            set => Record.BlogTags = value.Select(v => new BlogTag() { TagID = v }).ToList();
        }

            


    }
}
