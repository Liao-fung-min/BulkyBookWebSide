using BulkyBook.Models;

using Microsoft.AspNetCore.Mvc;

using BulkyBook.DataAccess;

namespace BulkyBookWeb.Controllers
{

    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        //建構子用來
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "輸入的順序不是相對應的名稱");

            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "類別新增成功";

                return RedirectToAction("Index");//返回到Index
            }
            else {
                TempData["error"] = "類別新增失敗";
            }
            return View(obj);

        }

        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }
            var categoryFromDb = _db.Categories.Find(id);
            //var category = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var category = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "輸入的順序不是相對應的名稱");

            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "類別更新成功";
               
                return RedirectToAction("Index");//返回到Index
            }
            else {
                TempData["error"] = "類別更新失敗";
            }
            return View(obj);

        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }
            var categoryFromDb = _db.Categories.Find(id);
            //var category = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var category = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //Post
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "類別刪除成功";
            }
            else {
                TempData["error"] = "類別刪除失敗";
            }
           
            
            return RedirectToAction("Index");

        }
    }
}
