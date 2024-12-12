using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using System.Linq;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class BlogController : MvcController
    {
        // Service injections:
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        private readonly ITagService _tagService;

        public BlogController(
			IBlogService blogService
            , IUserService userService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            , ITagService tagService
        )
        {
            _blogService = blogService;
            _userService = userService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            _tagService = tagService;
        }

        // GET: Blogs
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _blogService.Query().ToList();
            return View(list);
        }

        // GET: Blogs/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _blogService.Query().SingleOrDefault(q => q.Record.ID == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["UserID"] = new SelectList(_userService.Query().ToList(), "Record.ID", "Username");

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            ViewBag.TagIDs = new MultiSelectList(_tagService.Query().ToList(), "Record.ID", "Name");
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Blogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogModel blog)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _blogService.Create(blog.Record);
                if (result.IsSuccesful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = blog.Record.ID });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _blogService.Query().SingleOrDefault(q => q.Record.ID == id);
            SetViewData();
            return View(item);
        }

        // POST: Blogs/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BlogModel blog)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _blogService.Update(blog.Record);
                if (result.IsSuccesful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = blog.Record.ID });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _blogService.Query().SingleOrDefault(q => q.Record.ID == id);
            return View(item);
        }

        // POST: Blogs/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _blogService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
