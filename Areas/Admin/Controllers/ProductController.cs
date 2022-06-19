using BulkyBook.Models;

using Microsoft.AspNetCore.Mvc;

using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using BulkyBook.Models.ViewModels;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //建構子用來
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }


        //Get
        public IActionResult Upsert(int? id)
        {
            //Product product = new();
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            //{
            //    Text = c.Name,
            //    Value = c.Id.ToString(),

            //});
            //IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            //{
            //    Text = c.Name,
            //    Value = c.Id.ToString(),

            //});

            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()

                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()

                }),

            };

            if (id == null || id == 0)
            {
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CategoryList;
                //新增產品
                return View(productVM);

            }
            else
            {

                //更新產品
            }

            return View(productVM);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "輸入的順序不是相對應的名稱");

            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "類別更新成功";

                return RedirectToAction("Index");//返回到Index
            }
            else
            {
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
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbFist = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var category = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDbFist == null)
            {
                return NotFound();
            }
            return View(categoryFromDbFist);
        }

        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "類別刪除成功";
            }
            else
            {
                TempData["error"] = "類別刪除失敗";
            }


            return RedirectToAction("Index");

        }
    }
}
