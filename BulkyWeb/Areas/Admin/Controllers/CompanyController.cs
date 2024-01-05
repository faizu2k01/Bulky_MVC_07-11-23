using Bulky.DataAccess.RepositoryContainer.IRepositories;
using Bulky.Modals.ViewModels;
using Bulky.Modals;
using Bulky.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public IActionResult Index()
        {
            List<Company> objListData = _unitOfWork.Company.GetAll().ToList();
            return View(objListData);
        }

        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                return View(new Company());

            }
            else
            {
                Company company = _unitOfWork.Company.Get(x => x.Id == id);
                return View(company);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company payload, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (payload.Id == 0)
                {
                    _unitOfWork.Company.Add(payload);
                }
                else
                {
                    _unitOfWork.Company.Update(payload);
                }
                _unitOfWork.Company.Save();
                TempData["success"] = "Company created successfully";

                return RedirectToAction("Index", "Company");
            }
            else
            {

                return View(payload);

            }

        }






        #region Api
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objListData = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objListData });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToDel = _unitOfWork.Company.Get(x => x.Id == id);
            if (CompanyToDel == null)
            {
                return Json(new { success = false, message = "Error in deleting" });
            }


            _unitOfWork.Company.Remove(CompanyToDel);
            _unitOfWork.Company.Save();

            return Json(new { success = true, message = "Deleted Successfully" });

        }
        #endregion
    }

}
