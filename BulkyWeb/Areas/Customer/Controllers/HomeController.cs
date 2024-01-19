using Bulky.DataAccess.RepositoryContainer.IRepositories;
using Bulky.Modals;
using Bulky.Modals.ViewModels;
using Bulky.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimIdentifier = (ClaimsIdentity)User.Identity;
            var userId = claimIdentifier.FindFirst(ClaimTypes.NameIdentifier);

            if(userId != null)
            {
                 HttpContext.Session.SetInt32(SD.ShoppingCartItemsCount,
                _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == userId.Value).Count());
            }

            var productList = _unitOfWork.Product.GetAll(IncludeProps: "Category");
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            ShoppingCart shoppingCart = new()
            {
                Products = _unitOfWork.Product.Get(z => z.Id == id, "Category"),
                ProductId = id,
                Count = 1
            };
        
            return View(shoppingCart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimIdentifier = (ClaimsIdentity)User.Identity;
            var userId = claimIdentifier.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;
            shoppingCart.Id = 0;

            var productExi = _unitOfWork.ShoppingCart.Get(x => x.ProductId == shoppingCart.ProductId && x.ApplicationUserId == shoppingCart.ApplicationUserId);

            if(productExi != null)
            {
                productExi.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(productExi);
                _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.ShoppingCartItemsCount, _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == shoppingCart.ApplicationUserId).Count());
            }

            
            TempData["Success"] = "Cart added successfully";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}