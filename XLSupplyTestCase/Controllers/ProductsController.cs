using Manager.Dto;
using Manager.Service;
using Microsoft.AspNetCore.Http;
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

        public IActionResult Index(int id)
        {
            var result = _service.GetAllProductsList(id);
            return View(result);
        }
        public IActionResult MyFiles()
        {
            var memberId = HttpContext.Session.GetInt32("MemberId");
            var result = _service.GetMemberFile((int)memberId);
            return View(result);
        }
        public IActionResult AddFiles(AddMemberFileDto memberFileDto)
        {
            var memberId = HttpContext.Session.GetInt32("MemberId");
            memberFileDto.MemberId = (int)memberId;
            _service.AddMemberFile(memberFileDto);
            return RedirectToAction("MyFiles");
        }
    }
}
