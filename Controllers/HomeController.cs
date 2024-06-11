using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VnStockproxx.Models;

namespace VnStockproxx.Controllers
{
    public class HomeController : Controller
    {
        private readonly PostRepository _postRepo;
        private readonly CateRepository _cateRepo;

        public HomeController(VnStockproxxDbContext context)
        {
            this._postRepo = new PostRepository(context);
            this._cateRepo = new CateRepository(context);
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _cateRepo.GetAll();
            var posts = await _postRepo.GetAll();
            var lstNoiBat = from post in posts
                     join category in categories on post.CateId equals category.Id
                     where category.Name == "Tin nổi bật"
                     select new
                     {
                         Id = post.Id,
                         Title = post.Title,
                         Image = post.Image,
                         ViewCount = post.ViewCount,
                         CreatedDate = post.CreatedDate,
                         UpdatedDate = post.UpdatedDate,
                         CategoryName = category.Name
                     };
            var lstMoi = from post in posts
                    join category in categories on post.CateId equals category.Id
                    where category.Name == "Tin mới"
                    select new
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Image = post.Image,
                        ViewCount = post.ViewCount,
                        CreatedDate = post.CreatedDate,
                        UpdatedDate = post.UpdatedDate,
                        CategoryName = category.Name
                    };
            ViewBag.lstNoiBat = lstNoiBat.ToList();
            ViewBag.lstMoi = lstMoi.ToList();
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
