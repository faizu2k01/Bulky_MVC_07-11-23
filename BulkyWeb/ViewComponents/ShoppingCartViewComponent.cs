using Bulky.DataAccess.RepositoryContainer.IRepositories;
using Bulky.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyWeb.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimIdentifier = (ClaimsIdentity)User.Identity;
            var userId = claimIdentifier.FindFirst(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                if(HttpContext.Session.GetInt32(SD.ShoppingCartItemsCount)== null)
                {
                    HttpContext.Session.SetInt32(SD.ShoppingCartItemsCount,
                                  _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == userId.Value).Count());
                }
               
                return View(HttpContext.Session.GetInt32(SD.ShoppingCartItemsCount));
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
