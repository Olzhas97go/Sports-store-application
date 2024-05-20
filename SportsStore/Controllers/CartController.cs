using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.Repository;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private IStoreRepository repository;

        public CartController(IStoreRepository repository, Cart cart)
        {
            this.repository = repository;
            this.cart = cart;
        }

        public Cart cart { get; set; }

        [HttpGet]
        public IActionResult Index(string returnUrl)
        {
            return View(new CartViewModel
            {
                ReturnUrl = returnUrl ?? "/",
                Cart = this.HttpContext.Session.GetJson<Cart>("cart") ?? new Cart(),
            });
        }

        [HttpPost]
        public IActionResult Index(long productId, string returnUrl)    
        {
            Product? product = this.repository.Products.FirstOrDefault(p => p.ProductId == productId);

            if(product != null)
            {
                this.cart.AddItem(product, 1);

                return View(new CartViewModel
                {
                    Cart = this.cart,
                    ReturnUrl = returnUrl,
                });
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}
