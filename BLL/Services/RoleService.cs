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

    public interface IRoleService
    {
        public IQueryable<RoleModel> Query();
        public ServiceBase Create(Role Record);
        public ServiceBase Update(Role Record);
        public ServiceBase Delete(int ID);

    }


    public class RoleService : ServiceBase, IRoleService
    {
        public RoleService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Role Record)
        {
            if (_db.Roles.Any(s => s.Name.ToUpper() == Record.Name.ToUpper().Trim()))
                return Error("Role with the same name exists!");
            Record.Name = Record.Name?.Trim();
            _db.Roles.Add(Record);
            _db.SaveChanges();
            return Success("Role created successfully.");
        }

        public ServiceBase Delete(int ID)
        {
           var entity = _db.Roles.Include(s => s.Users).SingleOrDefault(s => s.ID == ID);
            if (entity == null)
                return Error("Role can't be found!");
            if (entity.Users.Any())
                return Error("Role has relational user!");
            _db.Roles.Remove(entity);
            _db.SaveChanges();
            return Success("Role deleted successfully.");


        }

        public IQueryable<RoleModel> Query()
        {
            return _db.Roles.OrderBy(s => s.Name).Select(s => new RoleModel() { Record = s});
            
        }

        public ServiceBase Update(Role Record)
        {
            if (_db.Roles.Any(s => s.ID != Record.ID && s.Name.ToUpper() == Record.Name.ToUpper().Trim()))
                return Error("Role with the same name exists!");
            var entity = _db.Roles.SingleOrDefault(s => s.ID == Record.ID);
            if (entity == null)
                return Error("Role can't be found");
            entity.Name = Record.Name?.Trim();
            _db.Roles.Update(entity);
            _db.SaveChanges();
            return Success("Role updated successfully.");
               
        }
    }
}
