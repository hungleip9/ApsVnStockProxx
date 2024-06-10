using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VnStockproxx.Models;

namespace VnStockproxx.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public ViewResult Index()
        {
            return View();
        }
        [Route("ChungKhoan")]
        public ViewResult ChungKhoan()
        {
            return View();
        }
        [Route("BatDongSan")]
        public ViewResult BatDongSan()
        {
            return View();
        }
        [Route("TaiChinh")]
        public ViewResult TaiChinh()
        {
            return View();
        }
        [Route("GiaiTri")]
        public ViewResult GiaiTri()
        {
            return View();
        }
        [Route("GioiThieu")]
        public ViewResult GioiThieu()
        {
            return View();
        }
        [Route("LienHe")]
        public ViewResult LienHe()
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
