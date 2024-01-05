using Bulky.DataAccess.Data;
using Bulky.DataAccess.RepositoryContainer.IRepositories;
using Bulky.Modals;
using Bulky.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objListData = _unitOfWork.Category.GetAll().ToList();
            return View(objListData);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category payload)
        {
            if (payload.Name == payload.DisplayName.ToString())
            {
                ModelState.AddModelError("Name", "Category name should not be same as display order");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(payload);
                _unitOfWork.Category.Save();
                TempData["success"] = "Category created successfully";

                return RedirectToAction("Index", "Category");
            }

            return View();


        }


        public IActionResult Edit(int? Id)
        {
            if (Id == null) return NotFound();

            var category = _unitOfWork.Category.Get(x => x.CategoryId == Id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category payload)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(payload);
                _unitOfWork.Category.Save();

                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index", "Category");
            }

            return View();


        }
        public IActionResult Delete(int? Id)
        {
            if (Id == null) return NotFound();

            var category = _unitOfWork.Category.Get(x => x.CategoryId == Id);
            if (category == null) return NotFound();


            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            if (Id == null) return NotFound();

            var category = _unitOfWork.Category.Get(x => x.CategoryId == Id);
            if (category == null) return NotFound();
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Category.Save();
            TempData["success"] = "Category deleted successfully";


            return RedirectToAction("Index");
        }
    }
}
