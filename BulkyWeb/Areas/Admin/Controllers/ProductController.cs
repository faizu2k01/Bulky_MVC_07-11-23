using Bulky.DataAccess.RepositoryContainer.IRepositories;
using Bulky.Modals;
using Bulky.Modals.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnv;
        private readonly IUnitOfWork _unitOfWork;
    public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnv)
    {
        _unitOfWork = unitOfWork;
        _webHostEnv = webHostEnv;
    }
    public IActionResult Index()
    {
        List<Product> objListData = _unitOfWork.Product.GetAll("Category").ToList();
        return View(objListData);
    }

    public IActionResult Upsert(int? id)
    {
            ProductViewModel productView = new()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.CategoryId.ToString()
                })
            };

            if(id == null || id == 0)
            {
            return View(productView);

            }
            else
            {
                productView.Product = _unitOfWork.Product.Get(x => x.Id == id);
                return View(productView);
            }
    }

    [HttpPost]
    public IActionResult Upsert(ProductViewModel payload,IFormFile? file)
    {
        if (ModelState.IsValid)
        {
                string wwwRootPath = _webHostEnv.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath,@"images\product");

                    if (!string.IsNullOrEmpty(payload.Product.ImageUrl))
                    {
                        var oldPath = Path.Combine(wwwRootPath, payload.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);

                        }
                    }

                    using(var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    payload.Product.ImageUrl = @"\images\product\" + fileName;
                }
                
                if(payload.Product.Id == 0)
                {
                  _unitOfWork.Product.Add(payload.Product);
                }
                else
                {
                    _unitOfWork.Product.Updata(payload.Product);
                }
                    _unitOfWork.Product.Save();
                     TempData["success"] = "Product created successfully";

                     return RedirectToAction("Index", "Product");
        }
        else
        {
            payload.CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
            {
               Text = x.Name,
               Value = x.CategoryId.ToString()
            });

            return View(payload);

        }

    }


   
    


        #region Api
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objListData = _unitOfWork.Product.GetAll("Category").ToList();
            return Json(new { data = objListData });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToDel = _unitOfWork.Product.Get(x => x.Id == id);
            if(productToDel == null)
            {
                return Json(new { success = false, message = "Error in deleting" });
            }

            var oldPath = Path.Combine(_webHostEnv.WebRootPath, productToDel.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);

            }

            _unitOfWork.Product.Remove(productToDel);
            _unitOfWork.Product.Save();

            return Json(new { success = true, message = "Deleted Successfully" });

        }
        #endregion
    }
}
