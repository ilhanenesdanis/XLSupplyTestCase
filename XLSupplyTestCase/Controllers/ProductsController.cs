using Manager.Service;
using Microsoft.AspNetCore.Mvc;

namespace XLSupplyTestCase.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IService _service;
        public ProductsController(IService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
