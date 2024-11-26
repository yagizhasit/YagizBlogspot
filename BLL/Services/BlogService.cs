using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IBlogService
    {
        public IQueryable<BlogModel> Query();
        public ServiceBase Create(Blog record);
        public ServiceBase Update(Blog record);
        public ServiceBase Delete(int ID);

    }


    public class BlogService : ServiceBase, IBlogService
    {
        public BlogService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Blog record)
        {
            if (_db.Blogs.Any(s => s.Title.ToUpper() == record.Title.ToUpper().Trim()))
                return Error("A blog with the same title already exists");
            record.Title = record.Title?.Trim();
            record.Content = record.Content?.Trim();
           
            
            _db.Blogs.Add(record);
            _db.SaveChanges();
            return Success("Blog created successfully.");

        }

        public ServiceBase Delete(int ID)
        {
            var entity = _db.Blogs.Include(s => s.BlogTags).SingleOrDefault(s => s.ID == ID);
            if (entity == null)
                return Error("Blog can't be found");
            if (entity.BlogTags.Any())
                return Error("Blog has relational blogtags!");
            _db.Blogs.Remove(entity);
            _db.SaveChanges();
            return Success("Blog deleted successfully.");

        }

        public IQueryable<BlogModel> Query()
        {
            return _db.Blogs.OrderBy(s => s.PublishDate).Select(s => new BlogModel() { Record = s });
        }

        public ServiceBase Update(Blog record)
        {
            if (_db.Blogs.Any(s => s.ID != record.ID && s.Title.ToUpper() == record.Title.ToUpper().Trim()))
                return Error("A blog with the same title already exists");
            var entity = _db.Blogs.FirstOrDefault(s => s.ID == record.ID);
            if (entity == null)
                return Error("Blog can't be found");
            entity.Title = record.Title?.Trim();
            entity.Content = record.Content;
            entity.Rating = record.Rating;
            entity.PublishDate = record.PublishDate;
            entity.BlogTags = record.BlogTags;
            _db.Blogs.Update(entity);
            _db.SaveChanges();
            return Success("Blog updated successfully.");
        }
    }
}
