using BookWebApp.Data;
using BookWebApp.Helpers;
using BookWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;

        }



        public IActionResult Index(int? pageNumber)
        {
            int pageSize = 5;
            //IEnumerable<Category> objCategoryList = _db.Categories;
            return View(PagedList<Category>.Create(_db.Categories.ToList(), pageNumber ?? 1, pageSize));
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString() && obj.Title == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name","The DisplayOrder cannot exactly match the key");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
                /* return RedirectToAction("Index", Controller name);*/
            }

            return View(obj);

        }


        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var CategoryFromDb = _db.Categories.Find(id);
            /* var CategoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);*/
            //var CategoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (CategoryFromDb == null)
            {
                return NotFound();
            }

            return View(CategoryFromDb);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the key");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
                /* return RedirectToAction("Index", Controller name);*/
            }

            return View(obj);

        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var CategoryFromDb = _db.Categories.Find(id);
            /* var CategoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);*/
            //var CategoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (CategoryFromDb == null)
            {
                return NotFound();
            }

            return View(CategoryFromDb);
        }


        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
            /* return RedirectToAction("Index", Controller name);*/

            /*return View(obj);*/

        }


    }

    
}


