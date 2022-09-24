using Manager.Dto;
using Manager.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;

namespace XLSupplyTestCase.Controllers
{
    public class LoginController : Controller
    {
        private readonly IService _service;

        public LoginController(IService service)
        {
            _service = service;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterDto registerDto)
        {
            _service.Register(registerDto);
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginDto login)
        {
            var MemberId = _service.Login(login);
            if (MemberId == 0)
            {
                return View();
            }
            HttpContext.Session.SetInt32("MemberId", MemberId);
            return RedirectToAction("MyFiles", "Products");
        }
    }
}
