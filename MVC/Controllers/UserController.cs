using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using BLL.DAL;

// Generated from Custom Template.

namespace MVC.Controllers
{
    [Authorize]
    public class UserController : MvcController
    {
        // Service injections:
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public UserController(
			IUserService userService
            , IRoleService roleService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _userService = userService;
            _roleService = roleService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Users
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _userService.Query().ToList();
            return View(list);
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var users = _userService.Query()
                .Where(u => u.Record.IsActive) 
                .ToList(); 

                var userModel = users.SingleOrDefault(u =>
                    u.Record.Username == user.Record.Username &&
                    u.Record.Password == user.Record.Password);


                if (userModel is not null)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, userModel.Username),
                        new Claim(ClaimTypes.Role, userModel.UserRole),
                        new Claim("ID", userModel.Record.ID.ToString())
                    };
                    var identity  = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal, new AuthenticationProperties()
                    {
                        IsPersistent = true
                    });
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        // GET: Users/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _userService.Query().SingleOrDefault(q => q.Record.ID == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Record.ID", "Name");

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Users/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _userService.Create(user.Record);
                if (result.IsSuccesful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = user.Record.ID });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _userService.Query().SingleOrDefault(q => q.Record.ID == id);
            SetViewData();
            return View(item);
        }

        // POST: Users/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(UserModel user)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _userService.Update(user.Record);
                if (result.IsSuccesful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = user.Record.ID });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _userService.Query().SingleOrDefault(q => q.Record.ID == id);
            return View(item);
        }

        // POST: Users/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _userService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
