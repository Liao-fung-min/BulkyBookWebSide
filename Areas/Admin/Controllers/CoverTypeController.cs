using BulkyBook.Models;

using Microsoft.AspNetCore.Mvc;

using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //建構子用來
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }
        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "類別新增成功";

                return RedirectToAction("Index");//返回到Index
            }
            else
            {
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
            
            var coverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
           

            if (coverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(coverTypeFromDbFirst);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
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

            var coverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);


            if (coverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(coverTypeFromDbFirst);
        }

        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Remove(obj);
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
