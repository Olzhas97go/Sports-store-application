using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.Repository;

namespace SportsStore.Controllers
{
  public class HomeController : Controller
    {
        private readonly IStoreRepository _repository;

        public HomeController(IStoreRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index() => View(_repository.Products);
    }
}
