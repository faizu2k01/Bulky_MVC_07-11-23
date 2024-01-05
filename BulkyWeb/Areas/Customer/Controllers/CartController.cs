using Bulky.DataAccess.RepositoryContainer.IRepositories;
using Bulky.DataAccess.RepositoryContainer.Repositories;
using Bulky.Modals;
using Bulky.Modals.ViewModels;
using Bulky.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ShoppingCartVM shoppingCartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimIdentifier = (ClaimsIdentity)User.Identity;
            var userId = claimIdentifier.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cartList = _unitOfWork.ShoppingCart.GetAll(filter:x => x.ApplicationUserId == userId,IncludeProps:"Products");

            shoppingCartVM = new()
            {
                ShoppingCartList = cartList,
                OrderHeader = new()
            };

            foreach(var item in shoppingCartVM.ShoppingCartList)
            {
                item.Price = GetOrderPrice(item);
                shoppingCartVM.OrderHeader.OrderTotal += (item.Count * item.Price);
            }
            
            return View(shoppingCartVM);
        }

        public IActionResult Summary()
        {
            var claimIdentifier = (ClaimsIdentity)User.Identity;
            var userId = claimIdentifier.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cartList = _unitOfWork.ShoppingCart.GetAll(filter: x => x.ApplicationUserId == userId, IncludeProps: "Products");

            shoppingCartVM = new()
            {
                ShoppingCartList = cartList,
                OrderHeader = new()
            };

            var applicationUser = _unitOfWork.ApplicationUserRepo.Get(x=> x.Id == userId);
            shoppingCartVM.OrderHeader = new()
            {
                Name = applicationUser.Name,
                PhoneNumber = applicationUser.PhoneNumber,
                StreetAddress = applicationUser.StreetName,
                City = applicationUser.City,
                State = applicationUser.State,
                PostalCode = applicationUser.PostalCode.ToString()
            };

            foreach (var item in shoppingCartVM.ShoppingCartList)
            {
                item.Price = GetOrderPrice(item);
                shoppingCartVM.OrderHeader.OrderTotal += (item.Count * item.Price);
            }

            return View(shoppingCartVM);
        }


        [HttpPost]
        [ActionName("Summary")]
		public IActionResult SummaryPOST()
		{
			var claimIdentifier = (ClaimsIdentity)User.Identity;
			var userId = claimIdentifier.FindFirst(ClaimTypes.NameIdentifier).Value;

			shoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: x => x.ApplicationUserId == userId, IncludeProps: "Products");
           
           
            var applicationUser = _unitOfWork.ApplicationUserRepo.Get(x => x.Id == userId);

			foreach (var item in shoppingCartVM.ShoppingCartList)
			{
				item.Price = GetOrderPrice(item);
				shoppingCartVM.OrderHeader.OrderTotal += (item.Count * item.Price);
			}
			shoppingCartVM.OrderHeader = new()
			{
				Name = applicationUser.Name,
				PhoneNumber = applicationUser.PhoneNumber,
				StreetAddress = applicationUser.StreetName,
				City = applicationUser.City,
				State = applicationUser.State,
				PostalCode = applicationUser.PostalCode.ToString()
			};

			shoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
			shoppingCartVM.OrderHeader.ApplicationUserId = userId;

			if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                shoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
                shoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
            }
            else
            {
				shoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
				shoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
			}

            _unitOfWork.OrderHeader.Add(shoppingCartVM.OrderHeader);
            _unitOfWork.Save();

            foreach(var item in shoppingCartVM.ShoppingCartList)
            {
                OrderDetail order = new()
                {
                    ProductId = item.ProductId,
                    OrderHeaderId = shoppingCartVM.OrderHeader.Id,
                    Price = item.Price,
                    Count = item.Count
                };

                _unitOfWork.OrderDetail.Add(order);
                _unitOfWork.Save();
            }

            if(applicationUser.CompanyId.GetValueOrDefault() == 0)
            {

            }
			return RedirectToAction(nameof(OrderConfirmation),new {id=shoppingCartVM.OrderHeader.Id});
		}

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }

		public IActionResult Plus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.Get(x => x.Id == cartId);
            cart.Count += 1;
            _unitOfWork.ShoppingCart.Update(cart);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.Get(x => x.Id == cartId);

            if(cart.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Remove(cart);

            }
            else
            {
                cart.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cart);

            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.Get(x => x.Id == cartId);
            
                _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


        private double GetOrderPrice(ShoppingCart model)
        {
            if(model.Count <= 50)
            {
                return model.Products.Price;
            }
            else
            {
                if(model.Count <= 100)
                {
                    return model.Products.Price50;
                }
                else
                {
                    return model.Products.Price100;
                }
            }
        }
    }
}
