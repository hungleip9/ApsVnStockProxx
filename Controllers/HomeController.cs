using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var categories = await _cateRepo.GetAll().ToListAsync();
            var posts = await _postRepo.GetAll().ToListAsync();
            var data = from post in posts
                    join category in categories on post.CateId equals category.Id
                    where category.Name == "Tin mới" || category.Name == "Tin nổi bật"
                    select new Post
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Image = post.Image,
                        ViewCount = post.ViewCount,
                        CreatedDate = post.CreatedDate,
                        UpdatedDate = post.UpdatedDate,
                        CategoryName = category.Name
                    };
            return View(data.ToList());
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
