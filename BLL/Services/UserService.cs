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
        public interface IUserService
        {
            public IQueryable<UserModel> Query();
            public ServiceBase Create(User Record);
            public ServiceBase Update(User Record);
            public ServiceBase Delete(int ID);
            public ServiceBase Login(User Record);
            public ServiceBase Register(User Record);
            public UserModel LoggedInUser { get; set; }

        }

        public class UserService : ServiceBase, IUserService
        {
            public UserService(Db db) : base(db)
            {
            }

        public ServiceBase Create(User Record)
            {
                if(_db.Users.Any(u => u.Username.ToUpper() == Record.Username.ToUpper().Trim() && u.IsActive))
                
                    return Error($"Active user with the same username (\"{Record.Username}\") exists!");

            Record.Username = Record.Username.Trim();

            Record.Password = Record.Password.Trim();

            _db.Users.Add(Record);

            _db.SaveChanges();

            return Success($"User with \"{Record.Username}\" created successfully.");

            }

            public ServiceBase Delete(int ID)
            {
            var entity = _db.Users.Include(s => s.Blogs).SingleOrDefault(s => s.ID == ID);
            if (entity == null)
                return Error("User can't be found.");
            if (entity.Blogs.Any())
                return Error("User has relational blogs.");
            _db.Users.Remove(entity);
            _db.SaveChanges();
            return Success($"User with \"{entity.Username}\" deleted successfully.");
            }

        public UserModel LoggedInUser { get; set; }
        public ServiceBase Login(User Record)
        {
            var entity = _db.Users.Include(u => u.Role).SingleOrDefault(u => u.Username == Record.Username && u.Password == Record.Password && u.IsActive);

            if (entity == null)
                return Error("Invalid username or password!");

            LoggedInUser = new UserModel()
            {
                Record = entity
            };

            return Success("User logged in successfully.");

        }

        public ServiceBase Register(User Record)
        {
            if (_db.Users.Any(u => u.Username.ToUpper() == Record.Username.ToUpper().Trim() && u.IsActive))
                return Error("Active user with the same username exists!");

            Record.Username = Record.Username.Trim();
            Record.Password = Record.Password.Trim();
            Record.IsActive = true;
            Record.RoleId = (int)Roles.User;
            _db.Users.Add(Record);
            _db.SaveChanges();
            return Success("User registered successfully.");
            
        }

        public IQueryable<UserModel> Query()
            {
            return _db.Users.Include(u => u.Role).OrderByDescending(u => u.IsActive).ThenBy(u => u.Username).Select(u => new UserModel() { Record = u });
            }

        public ServiceBase Update(User Record)
            {
            if (_db.Users.Any(u => u.ID != Record.ID && u.Username.ToUpper() == Record.Username.ToUpper().Trim() && u.IsActive))

                return Error($"Active user with the same username (\"{Record.Username}\") exists!");

            var entity = _db.Users.SingleOrDefault(u => u.ID == Record.ID);

            if (entity == null)

                return Error("User not found!");

            entity.Username = Record.Username.Trim();
            entity.Password = Record.Password.Trim();
            entity.IsActive = Record.IsActive;
            entity.RoleId = Record.RoleId;
            _db.Users.Update(entity);
            _db.SaveChanges();
            return Success($"User with \"{Record.Username}\" updated successfully.");
                
            }

        }
    }


  