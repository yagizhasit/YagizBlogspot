using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class RoleController : MvcController
    {
        // Service injections:
        private readonly IRoleService _roleService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public RoleController(
			IRoleService roleService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _roleService = roleService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Role
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _roleService.Query().ToList();
            return View(list);
        }

        // GET: Role/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _roleService.Query().SingleOrDefault(q => q.Record.ID == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _roleService.Create(role.Record);
                if (result.IsSuccesful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = role.Record.ID });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(role);
        }

        // GET: Role/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _roleService.Query().SingleOrDefault(q => q.Record.ID == id);
            SetViewData();
            return View(item);
        }

        // POST: Role/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _roleService.Update(role.Record);
                if (result.IsSuccesful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = role.Record.ID });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(role);
        }

        // GET: Role/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _roleService.Query().SingleOrDefault(q => q.Record.ID == id);
            return View(item);
        }

        // POST: Role/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _roleService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
