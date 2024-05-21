using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.Repository;

namespace SportsStore.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private IStoreRepository storeRepository;

        private IOrderRepository orderRepository;

        public AdminController(IStoreRepository storeRepository, IOrderRepository orderRepository)
            => (this.storeRepository, this.orderRepository) = (storeRepository, orderRepository);

        [Route("Orders")]
        public ViewResult Orders() => View(orderRepository.Orders);

        [Route("Products")]
        public ViewResult Products() => View(storeRepository.Products);

        [HttpPost]
        [Route("MarkShipped")]  
        public IActionResult MarkShipped(int orderId)
        {
            Order? order = orderRepository.Orders.FirstOrDefault(o => o.OrderId == orderId);

            if (order != null)
            {
                order.Shipped = true;
                orderRepository.SaveOrder(order);
            }

            return RedirectToAction("Orders");
        }

        [HttpPost]
        [Route("Reset")]
        public IActionResult Reset(int orderId)
        {
            Order? order = orderRepository.Orders.FirstOrDefault(o => o.OrderId == orderId);

            if (order != null )
            {
                order.Shipped = false;
                orderRepository.SaveOrder(order);
            }

            return RedirectToAction("Orders");
        }
    }
}
