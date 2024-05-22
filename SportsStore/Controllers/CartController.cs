using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.Repository;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
#pragma warning disable S6934 // A Route attribute should be added to the controller when a route template is specified at the action level
    public class CartController : Controller
#pragma warning restore S6934 // A Route attribute should be added to the controller when a route template is specified at the action level
    {
        private readonly IStoreRepository repository;

        public CartController(IStoreRepository repository, Cart cart)
        {
            this.repository = repository;
            this.Cart = cart;
        }

        public Cart Cart { get; set; }

        [HttpGet]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054", Justification = "//")]
        public IActionResult Index(string returnUrl)
        {
            return this.View(new CartViewModel
            {
                ReturnUrl = returnUrl ?? "/",
                Cart = this.Cart,
            });
        }

        [HttpPost]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054", Justification = "//")]
        public IActionResult Index(long productId, string returnUrl)
        {
            Product? product = this.repository.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                this.Cart.AddItem(product, 1);

                return this.View(new CartViewModel
                {
                    Cart = this.Cart,
                    ReturnUrl = returnUrl,
                });
            }

            return this.RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("Cart/Remove")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054", Justification = "//")]
        public IActionResult Remove(long productId, string returnUrl)
        {
            this.Cart.RemoveLine(this.Cart.Lines.First(cl => cl.Product.ProductId == productId).Product);
            return this.View("Index", new CartViewModel
            {
                Cart = this.Cart,
                ReturnUrl = returnUrl ?? "/",
            });
        }
    }
}
