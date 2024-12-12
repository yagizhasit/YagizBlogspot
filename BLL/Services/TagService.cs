using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{

    public interface ITagService
    {
        public IQueryable<TagModel> Query();
        public ServiceBase Create(Tag Record);
        public ServiceBase Update(Tag Record);
        public ServiceBase Delete(int ID);

    }

    public class TagService : ServiceBase, ITagService
    {
        public TagService(Db db) : base(db) { }

        public ServiceBase Create(Tag Record)
        {
            if (_db.Tags.Any(s => s.Name.ToUpper() == Record.Name.ToUpper().Trim()))
                return Error("Tag with the same name exists!");
            Record.Name = Record.Name?.Trim();
            _db.Tags.Add(Record);
            _db.SaveChanges();
            return Success("Tag Created Successfully!");

        }

        public ServiceBase Delete(int ID)
        {
            var entity = _db.Tags.Include(s => s.BlogTags).SingleOrDefault(s => s.ID == ID);
            if (entity == null)
                return Error("Tag can't be found!");
            if (entity.BlogTags.Any())
                return Error("Tag has relational Blogs!");
            _db.Tags.Remove(entity);
            _db.SaveChanges();
            return Success("Tag Deleted Successfully!");

        }

        public IQueryable<TagModel> Query()
        {
            return _db.Tags.OrderBy(s => s.Name).Select(s => new TagModel() { Record = s });

        }

        public ServiceBase Update(Tag Record)
        {
            if(_db.Tags.Any(s => s.ID != Record.ID && s.Name.ToUpper() == Record.Name.ToUpper().Trim()))
                return Error("Role with the same name exists!");
            var entity = _db.Tags.SingleOrDefault(s => s.ID == Record.ID);
            if (entity == null)
                return Error("Tag can't be found!");
            entity.Name = Record?.Name.Trim();
            _db.Tags.Update(entity);
            _db.SaveChanges();
            return Success("Tag updated successfully!");

        }


    }

}
