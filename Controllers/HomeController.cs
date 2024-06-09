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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("ChungKhoan")]
        public async Task<ViewResult> ChungKhoan()
        {
            return View();
        }
        [Route("BatDongSan")]
        public async Task<ViewResult> BatDongSan()
        {
            return View();
        }
        [Route("TaiChinh")]
        public async Task<ViewResult> TaiChinh()
        {
            return View();
        }
        [Route("GiaiTri")]
        public async Task<ViewResult> GiaiTri()
        {
            return View();
        }
        [Route("GioiThieu")]
        public async Task<ViewResult> GioiThieu()
        {
            return View();
        }
        [Route("LienHe")]
        public async Task<ViewResult> LienHe()
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
